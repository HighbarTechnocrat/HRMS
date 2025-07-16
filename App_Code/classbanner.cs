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

public struct banner
{
    public int bannerid;
    public string imagename;
    public string linkname;
    public string altname;
    public string displayposition;
    public string status;
      public string url;
    public string descupper;
    public string desclower;
}
/// <summary>
/// Summary description for classbanner
/// </summary>
public class classbanner
{
	public classbanner()
	{   
		//
		// TODO: Add constructor logic here
		//
	}
    //To get banner details from bannerid.
    public static banner getbanner(int bannerid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getbanner";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@bannerid";
        param.Value = bannerid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        //return GenericDataAccess.ExecuteSelectCommand(comm);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // wrap retrieved data into a details object
        banner details = new banner();
        if (table.Rows.Count > 0)
        {
            // get the first table row
            DataRow dr = table.Rows[0];
            // get banner details
            details.bannerid = Convert.ToInt32(dr["bannerid"]);
            details.imagename = dr["imagename"].ToString();
            details.linkname = dr["linkname"].ToString();
            details.altname = dr["altname"].ToString();
            details.displayposition = dr["displayposition"].ToString();
            details.status = Convert.ToString(dr["status"]);
            details.url = dr["url"].ToString();
            details.descupper = Convert.ToString(dr["descupper"]);
            details.desclower = Convert.ToString(dr["desclower"]);
        }
        //return banner details
        return details;
    }
    
    public static bool deletebanner(int bannerid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deletebanner";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@bannerid";
        param.Value = bannerid;
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
   //To create new banner.
    public static bool createbanner(string imagename,string linkname,string altname,string displayposition,string status,string url,string descUpper,string descLower)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_createupdatebanner";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@imagename";
        param.Value = imagename;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@linkname";
        param.Value = linkname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@altname";
        param.Value = altname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@displayposition";
        param.Value =displayposition;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@status";
        param.Value = status;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
          // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@url";
        param.Value =url;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@descupper";
        param.Value = descUpper;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@desclower";
        param.Value = descLower;
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

    public static bool insertcategorybanner(int bannerid, int categoryid,string createdby)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertcategorybanner";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@bannerid";
        param.Value = bannerid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
      
        param = comm.CreateParameter();
        param.ParameterName = "@createdby";
        param.Value = createdby;
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
    // Update an existing banner.
    public static bool updatebanner(int bannerid, string imagename, string linkname, string altname, string displayposition, string status,string url, string descUpper, string descLower)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_createupdatebanner";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@bannerid";
        param.Value = bannerid;
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
        param.ParameterName = "@linkname";
        param.Value = linkname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@altname";
        param.Value = altname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@displayposition";
        param.Value = displayposition;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@status";
        param.Value = status;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

         // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@url";
        param.Value = url;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@descupper";
        param.Value = descUpper;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@desclower";
        param.Value = descLower;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch
        {
        }
        return (result >= -1);
    }
    public static banner DisplyTopBanner()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getTopBanner";
        // execute the stored procedure and return the results
        // return faq list
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        banner details = new banner();
        if (table.Rows.Count > 0)
        {
            // get the first table row
            DataRow dr = table.Rows[0];
            // get product details
            details.imagename = dr["imagename"].ToString();
            details.linkname = dr["linkname"].ToString();
            details.altname = dr["altname"].ToString();
        }
        return details;

    }
    public static DataTable TopBanner(string descupper,string desclower,string displayposition,int count)
    {
      
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getTopBanner";
        DbParameter param = comm.CreateParameter();

        param.ParameterName = "@descupper";
        param.Value = descupper;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        param = comm.CreateParameter(); 
        param.ParameterName = "@desclower";
        param.Value = desclower;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        
        param = comm.CreateParameter(); 
        param.ParameterName = "@displayposition";
        param.Value = displayposition;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@count";
        param.Value = count;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);


        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static banner DisplyRightBanner()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getRightBanner";
        // execute the stored procedure and return the results
        // return faq list
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        banner bannerdetails = new banner();
        if (table.Rows.Count > 0)
        {
            // get the first table row
            DataRow dr = table.Rows[0];
            // get product details
            bannerdetails.imagename = dr["imagename"].ToString();
            bannerdetails.linkname = dr["linkname"].ToString();
            bannerdetails.altname = dr["altname"].ToString();
        }
        return bannerdetails;

    }
    public static DataTable RightBanner()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getRightBanner";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getbannercategory()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getbannercategory";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable getmiddlebannertop(string descupper, string desclower, string displayposition, int bnnumber)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getmiddlebannertop";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@desclower";
        param.Value = desclower;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
       

        param = comm.CreateParameter();
        param.ParameterName = "@descupper";
        param.Value = descupper;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@displayposition";
        param.Value = displayposition;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@count";
        param.Value = bnnumber;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);



        return GenericDataAccess.ExecuteSelectCommand(comm);
        
    }

    public static DataTable getallcategorybannerbybannerid(int bannerid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "getallcategorybannerbybannerid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@bannerid";
        param.Value = bannerid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
   
  public static DataTable getbannerdetailbyposition(int categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_getbannerdetailbyposition";
        DbParameter param = comm.CreateParameter();

        param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
        
        
    }
    public static bool deletecategoryfrombanner(int bannerid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deletecategoryfrombanner";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@bannerid";
        param.Value = bannerid;
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

    public static DataSet searchbannerimagebycat(string catname)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "Sp_proc_searchbannerimagebycat";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@catname";
        param.Value = catname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCmd(comm);
        // return shop details

    }

    public static DataTable getbottombannerdetail()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_banner_getbottombannerdetail";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataSet getbannerdetailxmlmiddle()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_banner_getbannerdetailxmlmiddle";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCmd(comm);
    }
}
