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

public struct enquiry
{
    public string  productname;
    public int enquiryid;
    public DateTime enquirydate;
    public string username;
    public string address;
    public string emailid;
    public string contactno;
    public int productid;
    public int quantity;
    public string description;
   
}
/// <summary>
/// Summary description for classenquiry
/// </summary>
public class classenquiry
{
    public classenquiry()
    {
        
    }
    // retrieve the list of enquiries in a enquiry table.
    public static DataTable getenquirylist()
    {
       
        DbCommand comm = GenericDataAccess.CreateCommand();       
        comm.CommandText = "sp_proc_getenquirylist";     
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getenquirylist1()
    {

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getenquirylist1";
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    // retrieve the details of enquiry by EnquiryID in a enquiry table.
   
    public static enquiry getenquirydetails1(int enquiryid)
    {

        DbCommand comm = GenericDataAccess.CreateCommand();

        comm.CommandText = "sp_proc_getenquirydetails1";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@enquiryid";
        param.Value = enquiryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);

        enquiry details = new enquiry();
        if (table.Rows.Count > 0)
        {
            details.enquirydate = Convert.ToDateTime(table.Rows[0]["enquirydate"]);
            details.username = table.Rows[0]["username"].ToString();
            details.address = table.Rows[0]["address"].ToString();
            details.emailid = table.Rows[0]["emailid"].ToString();
            details.contactno = table.Rows[0]["contactno"].ToString();
            details.productname = Convert.ToString(table.Rows[0]["productname"]);
            details.quantity = Convert.ToInt32(table.Rows[0]["quantity"]);
            details.description = table.Rows[0]["description"].ToString();
        }
        // return enquiry details
        return details;
    }

    public static enquiry getenquirydetails(int enquiryid)
    {
       
        DbCommand comm = GenericDataAccess.CreateCommand();
      
        comm.CommandText = "sp_proc_getenquirydetails";
       
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@enquiryid";
        param.Value = enquiryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
      
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
       
        enquiry details = new enquiry();
        if (table.Rows.Count > 0)
        {
            details.enquirydate = Convert.ToDateTime(table.Rows[0]["enquirydate"]);
            details.username = table.Rows[0]["username"].ToString();
            details.address = table.Rows[0]["address"].ToString();
            details.emailid = table.Rows[0]["emailid"].ToString();
            details.contactno = table.Rows[0]["contactno"].ToString();
            details.productname = Convert.ToString (table.Rows[0]["productname"]);
            details.quantity = Convert.ToInt32(table.Rows[0]["quantity"]);
            details.description = table.Rows[0]["description"].ToString();
        }
        // return enquiry details
        return details;
    }

    // retrieve the list of enquiries by ProductID in a enquiry table.
    public static DataTable getenquirybyproduct(int productid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getenquirybyproduct";
        // create a new parameter

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    // Delete Enquiries.
    public static bool deleteenquiry(int enquiryid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deleteenquiry";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@enquiryid";
        param.Value = enquiryid;
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
        return (result != -1);
    }
    public static bool createprodenquiry(string username, string address, string emailid, string contactno, int productid, string quantity, string description)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_prodenquiryinsert";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@address";
        param.Value = address;
        param.DbType = DbType.String;
        //param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = emailid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@contactno";
        param.Value = contactno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@quantity";
        param.Value = quantity;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@description";
        param.Value = description;
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


    public static bool createenquiry(string username, string address, string emailid, string contactno, int productid, string quantity, string description)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_enquiryinsert";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@address";
        param.Value = address;
        param.DbType = DbType.String;
        //param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = emailid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@contactno";
        param.Value = contactno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@quantity";
        param.Value = quantity;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@description";
        param.Value = description;
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

    //new method on 14 aug 2013

    public static bool deleteprodenquiry(int enquiryid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deleteprodenquiry";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@enquiryid";
        param.Value = enquiryid;
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
        return (result != -1);
    }

    public static DataTable getenquirylistbyid(int enquiryid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getenquirylistbyid";
        // create a new parameter

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@enquiryid";
        param.Value = enquiryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataSet getproductenquiry()
    {
        //get configured database command
        DbCommand comm = GenericDataAccess.CreateCommand();
        // get command text as stored procedure
        comm.CommandText = "sp_proc_getproductenquiry";
        //execute the stored procedure
        //retuen the results
        return GenericDataAccess.ExecuteSelectCmd(comm);

    }

    public static DataSet getproductenqurysearchbyemail(string email)//added by vijay on 10 th Jan
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getproductenquirysearchdetails";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@email ";
        param.Value = email;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCmd(comm);
    }

    public static DataTable getprodenquirybyemail(string useremail)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_searchprodenquiryByAlpha1";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@useremail";
        paramp.Value = useremail;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static DataTable prodenquirysearches(string useremail, string fromdate, string todate)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_prodenquiryserach";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        // create a new parameter      
        param.ParameterName = "@useremail";
        param.Value = useremail;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
       // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@fromdate";
        param.Value = fromdate;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@todate";
        param.Value = todate;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        //param = comm.CreateParameter();
        //param.ParameterName = "@name";
        //param.Value = name;
        //param.DbType = DbType.String;
        //comm.Parameters.Add(param);
      // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable getenquirycategory(int enquiryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "Sp_proc_Getenquirytcategory";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@enquiryid";
        paramp.Value = enquiryid;
        paramp.DbType = DbType.Int32;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    

}
