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

public struct productimage
{
    public int indexid;
    public int productid;
    public string imagename;
}
public class classproductimage
{
	public classproductimage()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //public static DataSet getphotogalleryimages(int indexid)//added by vijay on 11 th Jan
    //{
    //    DbCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = "sp_proc_getphotogalleryimages";
    //    DbParameter param = comm.CreateParameter();
    //    param.ParameterName = "@indexid ";
    //    param.Value = indexid;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);
    //    return GenericDataAccess.ExecuteSelectCmd(comm);
    //}

    public static DataSet latestgroupwall(int grpid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_gettopmywallforgroup";

        //create parameter

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@gid";
        param.Value = grpid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCmd(comm);
         
    }

    public static DataSet browserWallAllSearch(string cid, string pname, string gid, string user, string wishlistemail, string title, string sdate, string edate, int PageIndex, int PageSize, out int RecordCount)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_gettopmywallfullsearch";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@cid";
        param.Value = cid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@pname";
        param.Value = pname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@gid";
        param.Value = gid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = user;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = wishlistemail;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@title";
        param.Value = title;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@sdate";
        param.Value = sdate;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@edate";
        param.Value = edate;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@PageIndex";
        param.Value = PageIndex;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@PageSize";
        param.Value = PageSize;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

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

    public static DataSet latestgroupwall1(string gid, int PageIndex, int PageSize, out int RecordCount)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_gettopmywallforgroup1";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@gid";
        param.Value = gid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@PageIndex";
        param.Value = PageIndex;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@PageSize";
        param.Value = PageSize;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

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

    public static DataTable getcategorydetails(int categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getcategorydetails";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryId";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
         DataTable dtc = GenericDataAccess.ExecuteSelectCommand(comm);
         return dtc;

    }


    public static DataSet latestwallforallsearch1(string cid,string pname,string gid,string user,string wishlistemail, string title ,string sdate,string edate, int PageIndex, int PageSize, out int RecordCount)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_gettopmywallforcategory";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@cid";
        param.Value = cid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@pname";
        param.Value = pname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@gid";
        param.Value = gid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = user;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = wishlistemail;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@title";
        param.Value = title;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@sdate";
        param.Value = sdate;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@edate";
        param.Value = edate;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@PageIndex";
        param.Value = PageIndex;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@PageSize";
        param.Value = PageSize;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

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

    //

    public static DataSet allpostbyuser(string emailid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getpost_for_user";
        //create parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = emailid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCmd(comm);
    }


    public static DataSet allpostbyuser1(string emailid, string title,string catid,int PageIndex, int PageSize, out int RecordCount)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getpost_for_user1";
        //create parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = emailid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@title";
        param.Value = title;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@catid";
        param.Value = catid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@PageIndex";
        param.Value = PageIndex;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@PageSize";
        param.Value = PageSize;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

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

    public static DataSet allpostbyuser2(string emailid, string title, string catid, int PageIndex, int PageSize, out int RecordCount)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getpost_for_user2";
        //create parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = emailid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@title";
        param.Value = title;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@catid";
        param.Value = catid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@PageIndex";
        param.Value = PageIndex;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@PageSize";
        param.Value = PageSize;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

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


    public static DataTable latestimage(int pid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_postimgonwall";
        // execute the stored procedure and return the results
        // return enquiry list
        //return GenericDataAccess.ExecuteSelectCommand(comm);
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@catid";
        param.Value = pid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(comm);
        return dtc;
    }

    public static DataTable latestgroupimage(int pid,int grpid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_postgroupimgonwall";
        // execute the stored procedure and return the results
        // return enquiry list
        //return GenericDataAccess.ExecuteSelectCommand(comm);
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@catid";
        param.Value = pid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@gid";
        param.Value = grpid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(comm);
        return dtc;
    }


    public static DataTable latestads(int pid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_postadsonwall";
        // execute the stored procedure and return the results
        // return enquiry list
        //return GenericDataAccess.ExecuteSelectCommand(comm);
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@catid";
        param.Value = pid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(comm);
        return dtc;
    }

    public static DataTable latestgroupads(int pid, int grpid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_grouppostadsonwall";
        // execute the stored procedure and return the results
        // return enquiry list
        //return GenericDataAccess.ExecuteSelectCommand(comm);
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@catid";
        param.Value = pid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@gid";
        param.Value = grpid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(comm);
        return dtc;
    }

    public static DataTable latestvideo(int pid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_postvideoonwall";
        // execute the stored procedure and return the results
        // return enquiry list
        //return GenericDataAccess.ExecuteSelectCommand(comm);
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@catid";
        param.Value = pid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(comm);
        return dtc;
    }

    public static DataTable latestgroupvideo(int pid, int grpid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_grouppostvideoonwall";
        // execute the stored procedure and return the results
        // return enquiry list
        //return GenericDataAccess.ExecuteSelectCommand(comm);
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@catid";
        param.Value = pid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@gid";
        param.Value = grpid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(comm);
        return dtc;
    }

    public static DataTable latestfunzone()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_postfunonwall";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable latestgroupfunzone(int grpid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_grouppostfunonwall";
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@gid";
        param.Value = grpid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable latestdoc(int pid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_postdoconwall";

         //create parameter

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@catid";
        param.Value = pid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(comm);
        return dtc;
    }

    public static DataTable latestgrpdoc(int pid,int grpid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_grouppostdoconwall";

        //create parameter

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@catid";
        param.Value = pid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@gid";
        param.Value = grpid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(comm);
        return dtc;

    } 

    public static bool createproductimage(int productid, string imagename)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdateproductimage";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@imagename";
        param.Value = imagename;
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

    //to delete the image file from database

    public static bool deleteimagefile(int indexid)
    {
        //get configured db command

        DbCommand comm = GenericDataAccess.CreateCommand();

        // get command type as stored procedure

        comm.CommandText = "sp_proc_deleteproductimage";

        //create parameter

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@indexid";
        param.Value = indexid;
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
    
    public static DataTable loadvendorimagefiledtls(string vendor)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_loadvendorimagefiledtls";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@vendor";
        param.Value = vendor;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        //extcute the stored procedure and return result
        //return faq list based on faqcatid
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static DataTable displayimage(int productid)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_displayimage";
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
    public static bool createphogalary(string name, string imagename)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdateproductphotogallary1";
        // create a new parameter
        DbParameter param = comm.CreateParameter();

        param = comm.CreateParameter();
        param.ParameterName = "@imagename";
        param.Value = imagename;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@name";
        param.Value = name;
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

    public static bool updateproductimage(int indexid, string name,string imagename)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdateproductphotogallary";
        //// create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@indexid";
        param.Value = indexid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter

       
        param = comm.CreateParameter();
        param.ParameterName = "@name";
        param.Value = name;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter

        param = comm.CreateParameter();
        param.ParameterName = "@imagename";
        param.Value = imagename;
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

    public static DataTable loadimagefiledtls1()
    {
        //get configured database command
        DbCommand comm = GenericDataAccess.CreateCommand();
        // get command text as stored procedure
        comm.CommandText = "sp_proc_loadimagefiledtls1";
        //execute the stored procedure
        //retuen the results
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }


    public static bool updateproductimage1(int indexid, string imagename, string name)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdateproductphotogallary1";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@indexid";
        param.Value = indexid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@imagename";
        param.Value = imagename;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@name";
        param.Value = name;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        int result = -1;
        // try
        // {
        // execute the stored procedure
        result = GenericDataAccess.ExecuteNonQuery(comm);
        // }
        //  catch
        // {
        // any errors are logged in GenericDataAccess, we ingore them here
        //  }
        // result will be 1 in case of success 
        return (result != -1);
    }

    public static bool updateproductimage2(int productid, string bigimage)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_updateproductimage";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@imagename";
        param.Value = bigimage;
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

    public static DataTable getproductforprodimages()
    {

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_searchproductforimage";
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable getproductforimagesbysearch(string strprodname)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_searchproductforimagebytextsearch";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@prodname";
        param.Value = strprodname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getproductforimagebyprodid(int strprodid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getproductforimagebyprodid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@prodid";
        param.Value = strprodid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    //new method on 9thaug

    public static DataTable getproductidforproductimages(string search)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_searchprodidforprodimag";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@search";
        param.Value = search;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataSet getphotogalleryimages(int indexid)//added by vijay on 11 th Jan
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getphotogalleryimages";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@indexid ";
        param.Value = indexid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCmd(comm);
    }

    public static bool delphotogalleryimages(int indexid)//added by vijay on 11 th Jan
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_delphotogalleryimages";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@indexid ";
        param.Value = indexid;
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
        return (result >= 1);
    }





    public static DataSet latestwallforallsearch(string catid, char p1, char p2, char p3, char p4, char p5, int PageIndex, int PageSize, out int RecordCount)
    {
        throw new NotImplementedException();
    }
}
