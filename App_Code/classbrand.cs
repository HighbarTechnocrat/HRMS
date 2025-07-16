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


public struct brand
{
    public int brandid;
    public string brandname;
    public string colorName;
    public string Hex_Code;
    public string branddescription;
    public DateTime createdon;
    public string createdby;
    public DateTime modifiedon;
    public string modifiedby;
    public string  smallimage ;
    public char  showonHpage ;
    public char  fetured;
    public string bigimage;
    public int count;
}
/// <summary>
/// Summary description for classbrand
/// </summary>
public class classbrand
{
    public classbrand()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    // retrieve the list of brands in a Brand table.
    public static DataTable getbrandlist()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getbrandlist";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
	// added by Sony Surana as on 22 Nov 2012 for performance boosting
    public static DataTable vendoradmin_getbrandlist_by_vendor(string vendorname)
    {
       DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
	// commented by Sony Surana as on 22 Nov 2012 
    // comm.CommandText = "sp_proc_getnewbrandsbyvendor";
 	//called VIEW View_vendormodule_getbrands_category_vendorname through stored procedure
	// added by Sony Surana as on 22 Nov 2012 
		comm.CommandText = "usp_vendormodule_getnewbrandsbyvendor";

		
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure and save the results in a DataTable
        return GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
    }
		
    public static DataTable getbrandlist_add_product()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getbrandlist_Add_Product";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable gethomepagebrandlist()
    {
      
        DbCommand comm = GenericDataAccess.CreateCommand();      
        comm.CommandText = "sp_proc_gethomepagebrandlist";    
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    
    public static DataTable selectbrand()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "Sp_proc_Selectbrand";
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    // retrieve the details of brand by brandid in a Brand table.
    public static brand getbranddetails(int brandid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getbranddetails";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@brandid";
        param.Value = brandid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
      
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
      
        brand details = new brand();
        if (table.Rows.Count > 0)
        {           
            DataRow dr = table.Rows[0];           
            details.brandid = Convert.ToInt32(dr["brandid"]);
            details.brandname = dr["brandname"].ToString();
            details.branddescription = dr["branddescription"].ToString();
            details.smallimage = Convert.ToString(dr["smallimage"]);
            if (dr["showonHpage"] != DBNull.Value)
            {
                details.showonHpage = Convert.ToChar(dr["showonHpage"].ToString());
            }
            else
            {
              details.showonHpage ='N';
            }
            if (dr["fetured"] != DBNull.Value)
            {
                details.fetured = Convert.ToChar(dr["fetured"].ToString());
            }
            else
            { 
              details.fetured ='N';
            }
            details.bigimage = Convert.ToString( dr["bigimage"].ToString());
            details.count = 1;
        }
        else
        {
            details.count = 0;
        }
        return details;
       
    }



    // get brand by passing productid from Brand table.
    public static DataTable getbrandbyproduct(int productid)
    {
       
        DbCommand comm = GenericDataAccess.CreateCommand();      
        comm.CommandText = "sp_proc_getbrandbyproduct1";     
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getbrandbyproductID(int productid)
    {       
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getbrandbyproductidonly";     
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);     
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable getAllbrandbydevice(int deviceid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getAllBrandBydevice";
        // create a new parameter
        DbParameter paramc = comm.CreateParameter();
        paramc.ParameterName = "@deviceid";
        paramc.Value = deviceid;
        paramc.DbType = DbType.Int32;
        comm.Parameters.Add(paramc);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static DataTable getcategorybybrand(int brandid)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        commd.CommandText = "sp_proc_getcategoryidbybrandid";

        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@brandid";
        paramz.Value = brandid;
        paramz.DbType = DbType.Int32;
        commd.Parameters.Add(paramz);

        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);

        return dtc;
    }
     public static DataTable getproductbybrand(int brandid)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        commd.CommandText = "sp_proc_getproductidbybrandid ";

        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@brandid";
        paramz.Value = brandid;
        paramz.DbType = DbType.Int32;
        commd.Parameters.Add(paramz);

        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);

        return dtc;
    }
    public static bool adddevicetobrand(int brandid, int deviceid)
    {

        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertdevicetobrand";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@brandid";
        param.Value = brandid;
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


    public static bool deletebranddev(int deviceid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deletebranddev";
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

    public static bool createbrand(string brandname, string branddescription, string createdby , string smallimage  ,string  showonHpage , string fetured, string bigimage)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdatebrand";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@brandname";
        param.Value = brandname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@branddescription";
        param.Value = branddescription;
        param.DbType = DbType.String;
        //param.Size = 50;
        comm.Parameters.Add(param);
       
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@smallimage";
        param.Value = smallimage;
        param.DbType = DbType.String;      
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@showonHpage";
        param.Value = showonHpage;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@fetured";
        param.Value = fetured;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@bigimage";
        param.Value = bigimage;
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

    //Update Brands
    public static bool updatebrand(int brandid, string brandname, string branddescription ,  string smallimage  ,string  showonHpage , string fetured, string bigimage)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdatebrand";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@brandid";
        param.Value = brandid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@brandname";
        param.Value = brandname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@branddescription";
        param.Value = branddescription;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@smallimage";
        param.Value = smallimage;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@showonHpage";
        param.Value = showonHpage;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@fetured";
        param.Value = fetured;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@bigimage";
        param.Value = bigimage;
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

    // Delete Brands.
    public static int deletebrand(int brandid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deletebrand";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@brandid";
        param.Value = brandid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure; an error will be thrown by the
        // database in case the department has related categories, in which case
        // it is not deleted
        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch
        {
            // any errors are logged in GenericDataAccess, we ingore them here
        }
        // result will be 1 in case of success
        return result;
    }
    public static bool addbrandtoproduct(int productid, int brandid, string createdby)
    {

        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertbrandtoproduct";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        
        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@brandid";
        param.Value = brandid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        
        param = comm.CreateParameter();
        param.ParameterName = "@createdby";
        param.Value = createdby;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);

        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch
        {
            // any errors are logged in GenericDataAccess, we ingore them here
        }
        // result will be 1 in case of success 
        return (result != -1);
    }

    public static bool deletebrandfromproduct(int productid )
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deletebrandfromproduct";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
       
        // execute the stored procedure
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
    public static DataTable getnewbrands()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getnewbrands";
        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // calculate how many pages of products and set the out parameter
        // return the page of products
        return table;
    }
    public static DataTable getbrandbycategory(int categoryId)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getbrandbycategory";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryId";
        param.Value = categoryId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable getbrandbyAttributes(string categoryId,string atributeids)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getbrandbyAttributes";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryId";
        param.Value = categoryId;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@StringValue";
        param.Value = atributeids;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static bool addbrandtocategory(int categoryid, int brandid, string createdby)
    {

        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertbrandtocategory";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryId";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
       
        param = comm.CreateParameter();
        param.ParameterName = "@brandId";
        param.Value = brandid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@createdby";
        param.Value = createdby;
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
    public static bool deletebrandfromcategory(int categoryid, int brandid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deletebrandfromcategory";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryId";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@brandId";
        param.Value = brandid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
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

    public static bool deleteallbrandfromcategory(int categoryid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deleteallbrandfromcategory";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryId";
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


    public static bool deleteprod_brand(int productid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deleteproduct_brand";
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
    public static DataTable GetDeviceName()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_selectdevicetypes";
        return GenericDataAccess.ExecuteSelectCommand(comm);
       
    }
    public static int addbranddevice(int brandid   ,int deviceid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_insertupdatebranddevice";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@brandid";
        param.Value = brandid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param); 

        param = comm.CreateParameter();
        param.ParameterName = "@DeviceID";
        param.Value = deviceid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        int result = 0;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch 
        { 
        }
        return result;

    }
    public static DataTable GetMaxBrandId()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_GetMaxBrandId";
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static bool deletebranddevice(int brandid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_deletebranddevice";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@brandid";
        param.Value = brandid;
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
    public static DataTable Getbranddeviceedit(int brandid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_Getbranddeviceedit";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@brandid";
        param.Value = brandid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable getbrandsearch(string strBrandname)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getbrandsearch";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@brandname";
        param.Value = strBrandname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static bool deletebrandfromcategory(int brandid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deletecategoryfrombrand";
        DbParameter param = comm.CreateParameter();
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@brandId";
        param.Value = brandid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
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

    public static DataTable getbrandimage(int brandid)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        commd.CommandText = "sp_proc_getbrandimage ";

        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@brandid";
        paramz.Value = brandid;
        paramz.DbType = DbType.Int32;
        commd.Parameters.Add(paramz);

        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);

        return dtc;
    }

    public static bool deletebrand1(int brandid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_deletebrand1";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@brandid";
        param.Value = brandid;
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


    public static DataTable getnewbrandsbyvendor(string vendorname)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
	// commented by Sony Surana as on 22 Nov 2012 
    // comm.CommandText = "sp_proc_getnewbrandsbyvendor";
 	//called VIEW View_vendormodule_getbrands_category_vendorname through stored procedure
	// added by Sony Surana as on 22 Nov 2012 
		comm.CommandText = "usp_vendormodule_getnewbrandsbyvendor";

		
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure and save the results in a DataTable
        return GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products

    }
    
}
