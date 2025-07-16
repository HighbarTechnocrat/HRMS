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



public struct product
{
    public int productid;
    public string productguid;
    public string productname;

    public string productnumber;
    public string shortdescription;
    public string longdescription;
    public string attributexml;
    public decimal ourprice;
    public decimal retailprice;
    public string smallimage;
    public string bigimage;
    public char viewstatus;
    public char productstatus;
    public int visitcount;
    public decimal shippingrate;
    public string shipday;
    public decimal CODRate;
    public string CODFlag;
    public decimal gfwraprate;
    public string gfwrapFlag;
    public string inventorytype;

    public DateTime createdon;
    public string createdby;
    public DateTime modifiedon;
    public string modifiedby;
    public int stockvalue;
    public string stockflag;
    public string auctionflag;
    public decimal startbid;
    public DateTime openfrom;
    public DateTime endon;
    public string status;


    public string producttitle;
    public char auctionflag1;
    public decimal StartBid;
    public DateTime OpenFrom;
    public DateTime Endon;
    public string Status;
    public int count;

    public decimal gftcoupondisc;


}

/// <summary>
/// Summary description for classproduct
/// </summary>
public class classproduct
{
    public classproduct()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public static product GetProductDetailsnew(int productId)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetProductDetails1";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // wrap retrieved data into a ProductDetails object
        product details = new product();
        if (table.Rows.Count > 0)
        {
            // get the first table row
            DataRow dr = table.Rows[0];
            // get product details
            details.productid = Convert.ToInt32(dr["productid"]);
            details.productname = dr["productName"].ToString();
            details.producttitle = dr["productName"].ToString();
            details.productnumber = Convert.ToString(dr["productnumber"]);
            details.shortdescription = dr["shortDescription"].ToString();
            details.longdescription = dr["longDescription"].ToString();
            details.ourprice = Decimal.Parse(dr["ourPrice"].ToString());
            details.retailprice = Decimal.Parse(dr["retailprice"].ToString());
            details.smallimage = dr["smallimage"].ToString();
            details.bigimage = dr["bigimage"].ToString();
            details.viewstatus = Convert.ToChar(dr["viewstatus"]);
          
            details.visitcount = Convert.ToInt32(dr["visitcount"]);
            details.shippingrate = Convert.ToDecimal(dr["shippingrate"]);
            details.shipday = Convert.ToString(dr["shipday"]);
            details.CODFlag = Convert.ToString(dr["CODChargeFlag"]);
            if (dr["CODCharge"] != DBNull.Value)
            {
                details.CODRate = Convert.ToDecimal(dr["CODCharge"]);
            }
            else
            {
                details.CODRate = 0;
            }
           
            if (dr["stockValue"] != DBNull.Value)
            {
                details.stockvalue = Convert.ToInt32(dr["stockValue"]);
            }
            else
            {
                details.stockvalue = 0;
            }

           
           
            if (dr["status"] != DBNull.Value)
            {
                details.status = Convert.ToString(dr["status"]);
            }
            else
            {
                details.status = "S";
            }
          
            details.count = 1;
        }
        else
        {
            details.count = 0;
        }
        // return department details
        return details;
    }

    public static DataTable GetAllChildData(int pid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getChildDeatils";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = pid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable GetWall()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getwall";
        // create a new parameter
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataSet getsearchlistbyDeviceName(string Name)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getsearchlistbyDeviceName";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@Name";
        param.Value = Name;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCmd1(comm);
    }


    public static DataTable getuserbypost(int pid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getuserByproductID_post";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = pid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable category_Menu(int parentid)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_category_Menu";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@parentid";
        param.Value = parentid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    //added by sapna ranaware for showing product features
    public static DataTable get_proc_ProductDescription_ProdFeature(int productid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText ="sp_proc_ProductDescription_ProdFeature";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName ="@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable get_productimage(int productid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getProductimage";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    //added by sapna ranaware on 2013-08-02 for showing big image
    public static DataTable getproductbigimage(int productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_GetProductDetails";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid ";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }
    //added on 2013-06-10

    public static DataTable getmultiplepostimage(int productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getmultiplepostimage";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid ";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }

    public static DataTable getmultiplepostimage1(int productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getproductdetails_multiimage";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid ";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }


    public static DataTable getmultipleimages(int productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getmultipleimages";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid ";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }

    public static bool createrating(int productid, string customername, int rating, string createdby)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_ratinginsert";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        //param.Size = 50;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = customername;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter       
        param = comm.CreateParameter();
        param.ParameterName = "@rating";
        param.Value = rating;
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
    public static DataTable getallnewproduct()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_product_getallnewproduct";
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }


    public static DataTable getavgrating(int productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getavgrating";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid ";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getnextproductinfo(int productid, string flag, string flag_vmn)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_product_getnextproductinfo";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productno";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@flag";
        param.Value = flag;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@flag_vmn";
        param.Value = flag_vmn;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;
    }

 
    //added by sapna ranaware on 2013-06-20 for  fecthing next and previous feature product
    public static DataTable getnextfeatureproductinfo(int productid, string flag)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_product_getnextfeatureproductinfo";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productno";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@flag";
        param.Value = flag;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;
    }
    //added by sapna ranaware on 2013-06-20 for  fecthing next and previous related product
    public static DataTable getrelatedproductinfo(int productid, string flag,int mainprodid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_product_getrelatedproductinfo";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productno";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@flag";
        param.Value = flag;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@mainprodid";
        param.Value = mainprodid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;
    }
    //added by sapna ranawre on 2013-06-13  for breadcrum 
    public static DataTable getproductdetails(int productid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getproductdetails";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);


    }
    //added by sapna ranawre on 2013-06-13  for related product
    public static DataTable UserGetRelatedProducts(int productid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_usergetrelatedproducts";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ProductID";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // calculate how many pages of products and set the out parameter
        // return the page of products
        return table;
    }

    //added by sapna ranware on 2013-06-14 for recently viewd product

    public static DataTable getrecentlyviewProducts(string strproductid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_getrecentlyviewProducts";
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@productid";
        paramp.Value = strproductid;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    //added by sapna ranware on 2013-06-14 for feature product
    public static DataTable GetFeaturedProducts()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getfeaturedproduct";

        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // calculate how many pages of products and set the out parameter
        // return the page of products
        return table;
    }


    public static DataTable Getmostviewed()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getmostviewed";

        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // calculate how many pages of products and set the out parameter
        // return the page of products
        return table;
    }
   //******************************************wishlist***************************************
    public static DataTable getaddtowishProduct(Guid userid,string ipaddress,int productid,Decimal qty)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_insertupdatewishlist";
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@userid";
        paramp.Value = userid;
        paramp.DbType = DbType.Guid;
        comm.Parameters.Add(paramp);

        paramp = comm.CreateParameter();
        paramp.ParameterName = "@ipaddress";
        paramp.Value = ipaddress;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);

        paramp = comm.CreateParameter();
        paramp.ParameterName = "@productid";
        paramp.Value = productid;
        paramp.DbType = DbType.Int32;
        comm.Parameters.Add(paramp);

        paramp = comm.CreateParameter();
        paramp.ParameterName = "@qty";
        paramp.Value = qty;
        paramp.DbType = DbType.Decimal;
        comm.Parameters.Add(paramp);
       
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable del_wishlist(int productid)
    {
        DbCommand command = GenericDataAccess.CreateCommand();
        command.CommandText = "sp_proc_wishlist_delete";

        DbParameter param = command.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        command.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(command);
    }
   
    public static DataTable UserGetwishlistProducts(String userid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_wishlist_getproduct";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@userid";
        param.Value = userid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // calculate how many pages of products and set the out parameter
        // return the page of products
        return table;
    }

    //***********************************************buynow**************************************
    //added by sapna ranaware on 2013-08-02 for buynow 
    public static bool addtocartProduct(Guid userid, string ipaddress, int productid, Decimal qty,Decimal subtotal)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdateaddtocart";

        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@userid";
        paramp.Value = userid;
        paramp.DbType = DbType.Guid;
        comm.Parameters.Add(paramp);

        paramp = comm.CreateParameter();
        paramp.ParameterName = "@ipaddress";
        paramp.Value = ipaddress;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);

        paramp = comm.CreateParameter();
        paramp.ParameterName = "@productid";
        paramp.Value = productid;
        paramp.DbType = DbType.Int32;
        comm.Parameters.Add(paramp);

        paramp = comm.CreateParameter();
        paramp.ParameterName = "@qty";
        paramp.Value = qty;
        paramp.DbType = DbType.Decimal;
        comm.Parameters.Add(paramp);

        paramp = comm.CreateParameter();
        paramp.ParameterName = "@subtotal";
        paramp.Value = subtotal;
        paramp.DbType = DbType.Decimal;
        comm.Parameters.Add(paramp);
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


    //update qty in cart

    public static bool updatecartProduct(int indexid,decimal qty,decimal subtotal)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_updateaddtocart";

        // create a new parameter
        DbParameter paramp = comm.CreateParameter();

        // create a new parameter
        paramp = comm.CreateParameter();
        paramp.ParameterName = "@indexid";
        paramp.Value = indexid;
        paramp.DbType = DbType.Int32;
        comm.Parameters.Add(paramp);
        
        paramp = comm.CreateParameter();
        paramp.ParameterName = "@qty";
        paramp.Value = qty;
        paramp.DbType = DbType.Decimal;
        comm.Parameters.Add(paramp);

        paramp = comm.CreateParameter();
        paramp.ParameterName = "@subtotal";
        paramp.Value = subtotal;
        paramp.DbType = DbType.Decimal;
        comm.Parameters.Add(paramp);
        // result will represent the number of changed rows
        int result = -1;
        //try
        //{
        // execute the stored procedure
        result = GenericDataAccess.ExecuteNonQuery(comm);
        //}
        //catch
        //{
        // any errors are logged in GenericDataAccess, we ingore them here
        // }
        // result will be 1 in case of success 
        return (result >= 1);
    }

    //added by sapna ranaware on 2013-08-03 for getting buy product

    public static DataTable UserGetbuynnowProducts(String userid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_buynow_getproduct";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@userid";
        param.Value = userid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // calculate how many pages of products and set the out parameter
        // return the page of products
        return table;
    }


    //deleting product from cart of that particular user
    public static bool Deleteproductcart(string UserId, int ProductId)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_cart_Deleteproductcart";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@UserId";
        param.Value = UserId;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@ProductId";
        param.Value = ProductId;
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
    public static product getproductimage(int productid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getproductimage";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productcatid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // wrap retrieved data into a ProductDetails object
        product details = new product();
        if (table.Rows.Count > 0)
        {
            // get the first table row
            DataRow dr = table.Rows[0];
            // get product details
            details.productid = Convert.ToInt32(dr["productid"]);
            //details.productguid = dr["productguid"].ToString();
            details.smallimage = Convert.ToString(dr["smallimage"]);
            details.stockvalue = Convert.ToInt32(dr["stockValue"]);
            if (dr["stockValue"] != DBNull.Value)
            {
                details.stockvalue = Convert.ToInt32(dr["stockValue"]);
            }
            else
            {
                details.stockvalue = 0;
            }

            details.stockflag = Convert.ToString(dr["stockflag"]);

            details.gftcoupondisc = Convert.ToDecimal(dr["giftdiscount"]);
        }
        return details;
    }

    public static string getCODchargesbyproductid(int productid, int quantity)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_getCODchargesbyproductid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@quantity";
        param.Value = quantity;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteScalar(comm);

    }

    public static DataTable getGiftWrapbyproductid(int productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_getGiftWrapsbyproductid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;

    }

    public static DataTable getGiftWrapAmountbyproductid(int productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_getGiftWrapAmountbyproductid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;

    }
    public static int checkcodchagestomultipleproduct(string productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_checkcodchagestomultipleproduct";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        int val = Convert.ToInt32(GenericDataAccess.ExecuteScalar(comm));
        return val;
    }
    public static int checkcodproductinCOD(string productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_checkcodproductinCOD";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        int val = Convert.ToInt32(GenericDataAccess.ExecuteScalar(comm));
        return val;
    }

    //added by sapna ranaware on 2013-08-07 for checking duplicate entry of buy now product of particular user
    public static DataTable getbuynowproduct(int productid, string userid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_buynow_getproductinfo";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@userid";
        param.Value = userid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        
        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;
    }

    /*created by runila on 25-09-2012 for redeempoints*/
    public static string getredeempoints(string UserName)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getredeempoints";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@UserName";
        param.Value = UserName;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteScalar(comm);

    }
    /*Ended by runila*/
    public static string getredeempoints1(string UserName)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getredeempoints1";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@UserName";
        param.Value = UserName;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteScalar(comm);

    }
    //added by sapna ranaware on 2013-08-12 to get count of items in cart
    public static DataTable getcartitemcount(string userid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_buynow_getitemcount";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        
        param = comm.CreateParameter();
        param.ParameterName = "@userid";
        param.Value = userid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;
    }

    public static DataTable getwishlistitemcount(string userid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_wishlist_getwishlistcount";
        // create a new parameter
        DbParameter param = comm.CreateParameter();

        param = comm.CreateParameter();
        param.ParameterName = "@userid";
        param.Value = userid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;
    }
    public static DataTable getwishproduct(int productid, string userid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_wishlist_getproductwishlist";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@userid";
        param.Value = userid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;
    }
    public static DataTable getMaxMinCodShippingValue(decimal TotalAmount, string FlagCodShipping)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_CODRangeFromAmount";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@Amount";
        param.Value = TotalAmount;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@ShipCodFlag";
        param.Value = FlagCodShipping;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure and save the results in a DataTable
        return GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products

    }

    public static DataTable getMaxCODAmountMsgDisplay(decimal TotalAmount, string FlagCodShipping)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_CODMSgFromAmount";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@Amount";
        param.Value = TotalAmount;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@ShipCodFlag";
        param.Value = FlagCodShipping;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure and save the results in a DataTable
        return GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products

    }
    public static string getshipratebyprdidandupdatestockcounts(int productid)
    {

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_getshipratebyprdidandupdatestockcounts";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);



        string details = GenericDataAccess.ExecuteScalar(comm);

        return details;
    }

    public static DataTable getattributfromview(string CategoryId, string auctionflag, int AttributeGroupID, string AttributeIds)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_getattributfromview";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@CategoryId ";
        param.Value = CategoryId;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        if (auctionflag != "N")
        {
            param = comm.CreateParameter();
            param.ParameterName = "@auctionflag";
            param.Value = auctionflag;
            param.DbType = DbType.String;
            comm.Parameters.Add(param);
        }
        else
        {
            param = comm.CreateParameter();
            param.ParameterName = "@auctionflag";
            param.Value = DBNull.Value;
            param.DbType = DbType.String;
            comm.Parameters.Add(param);
        }
        param = comm.CreateParameter();
        param.ParameterName = "@AttributeGroupID";
        param.Value = AttributeGroupID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@AttributeStr";
        param.Value = AttributeIds;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }

    public static DataTable getattributcountbybrand(string CategoryId, string auctionflag, int AttributeID, string brandid, string AttributeStr)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_getattributcountbybrand";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@CategoryId ";
        param.Value = CategoryId;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@AttributeGroupID";
        param.Value = AttributeID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@brandid";
        param.Value = brandid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@AttributeStr";
        param.Value = AttributeStr;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        if (auctionflag != "N")
        {
            param = comm.CreateParameter();
            param.ParameterName = "@auctionflag";
            param.Value = auctionflag;
            param.DbType = DbType.String;
            comm.Parameters.Add(param);
        }
        else
        {
            param = comm.CreateParameter();
            param.ParameterName = "@auctionflag";
            param.Value = DBNull.Value;
            param.DbType = DbType.String;
            comm.Parameters.Add(param);
        }


        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }
    public static DataTable getattributfromviewImg(string CategoryId, string auctionflag, int AttributeGroupID, string AttributeIds)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_getattributfromviewImg";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@CategoryId ";
        param.Value = CategoryId;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        if (auctionflag != "N")
        {
            param = comm.CreateParameter();
            param.ParameterName = "@auctionflag";
            param.Value = auctionflag;
            param.DbType = DbType.String;
            comm.Parameters.Add(param);
        }
        else
        {
            param = comm.CreateParameter();
            param.ParameterName = "@auctionflag";
            param.Value = DBNull.Value;
            param.DbType = DbType.String;
            comm.Parameters.Add(param);
        }
        param = comm.CreateParameter();
        param.ParameterName = "@AttributeGroupID";
        param.Value = AttributeGroupID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@AttributeStr";
        param.Value = AttributeIds;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }

    public static DataTable getattributcountbybrandImg(string CategoryId, string auctionflag, int AttributeID, string brandid, string AttributeStr)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_getattributcountbybrandImg";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@CategoryId ";
        param.Value = CategoryId;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@AttributeGroupID";
        param.Value = AttributeID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@brandid";
        param.Value = brandid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@AttributeStr";
        param.Value = AttributeStr;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        if (auctionflag != "N")
        {
            param = comm.CreateParameter();
            param.ParameterName = "@auctionflag";
            param.Value = auctionflag;
            param.DbType = DbType.String;
            comm.Parameters.Add(param);
        }
        else
        {
            param = comm.CreateParameter();
            param.ParameterName = "@auctionflag";
            param.Value = DBNull.Value;
            param.DbType = DbType.String;
            comm.Parameters.Add(param);
        }


        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }
    public static DataTable getproductfeaturehome()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getproductfeaturehome";
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }

    public static DataTable getmetaproductdetails(int productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getmetaproductdetails";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getmetacategorydetails(int categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getmetacategorydetails";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@catid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getcategorybyiddetails(int categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getcategorybyiddetails";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@catid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable selectcategory(int categoryid)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_geteventtypelists";
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

    public static DataTable getparentbycategoryid()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_category_getcategoryid1";
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }

    public static DataTable getproductlistdetails(int categoryid)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_getproductlistdetails";
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


    public static DataTable getproductfeaturexml()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getproductfeaturexml";
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }

    public static DataTable getattributebyid(string attid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getattributebyid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@attid";
        param.Value = attid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    //new method on 28th may
    public static DataTable getprodattrgrpid_variant(string strprodid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();

        comm.CommandText = "sp_getcombgrpid_product_combinations";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = strprodid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getchildprodid_product(string strgrpid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();

        comm.CommandText = "sp_getchildprodid_product";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productgrpid ";
        param.Value = strgrpid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    //new method on 26th

    public static DataTable getnewproductonhome()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getmoviefromproducts";
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }


    public static DataTable get_proc_Productattributes_product(int productid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_ProductDescription_ProdFeature";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }



    public static DataTable get_proc_Productrelated(int productid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
       // comm.CommandText = "sp_proc_getmoviefromproducts_relatedproduct";
        comm.CommandText = "sp_proc_usergetrelatedproducts";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable get_proc_moviedirector(int productid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = " sp_proc_getdirectornamebyprodid";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable get_proc_movieactorsproducer(int productid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = " sp_proc_movieactorsproducer";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }



    public static bool createviewscnt(decimal productid, decimal totalview)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertvideocnt";

        DbParameter param = comm.CreateParameter();
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@video_viewcount";
        param.Value = totalview;
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

    //new method on 4th oct

    public static DataTable getpopularproductonhome()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getpopularmovie_products";
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }


    public static DataTable get_proc_movietrailor_productid(int productid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_productvideotrailor_id";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataSet gettopmywall()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_gettopmywall";
        // execute the stored procedure and save the results in a DataTable
        DataSet ds = GenericDataAccess.ExecuteSelectCmd(comm);
        // return the page of products
        return ds;
    }
    public static DataTable gettopidproduct()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_gettopid_product";
        // create a new parameter

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static bool inserttextpost(string productname, string productnumber, string shortdescription, char viewstatus, char showonstatus, string createdby, string sdate, string edate)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insert_textpost";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productname";
        param.Value = productname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@productnumber";
        param.Value = productnumber;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@shortdescription";
        param.Value = shortdescription;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@viewstatus";
        param.Value = viewstatus;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@showonstatus";
        param.Value = showonstatus;
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
        param.ParameterName = "@startdate";
        param.Value = sdate;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@enddate";
        param.Value = edate;
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

    public static bool insertdocpost(string productname, string productnumber, string shortdescription, char viewstatus, char showonstatus, string createdby, string filename, string sdate, string edate)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insert_filepost";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productname";
        param.Value = productname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@productnumber";
        param.Value = productnumber;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@shortdescription";
        param.Value = shortdescription;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@viewstatus";
        param.Value = viewstatus;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@showonstatus";
        param.Value = showonstatus;
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
        param.ParameterName = "@filename";
        param.Value = filename;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@startdate";
        param.Value = sdate;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@enddate";
        param.Value = edate;
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


    public static bool insertvideopost(string productname, string productnumber, string shortdescription, char viewstatus, char showonstatus, string createdby, string videoembed, string sdate, string edate,string bigimg)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insert_videopost";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productname";
        param.Value = productname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@productnumber";
        param.Value = productnumber;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@shortdescription";
        param.Value = shortdescription;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@viewstatus";
        param.Value = viewstatus;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@showonstatus";
        param.Value = showonstatus;
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
        param.ParameterName = "@videoembed";
        param.Value = videoembed;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@startdate";
        param.Value = sdate;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@enddate";
        param.Value = edate;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@bigimg";
        param.Value = bigimg;
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

    public static bool insertimagepost(string productname, string productnumber, string shortdescription, char viewstatus, char showonstatus, string createdby, string bigimg, string sdate, string edate)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insert_imagepost";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productname";
        param.Value = productname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@productnumber";
        param.Value = productnumber;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@shortdescription";
        param.Value = shortdescription;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@viewstatus";
        param.Value = viewstatus;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@showonstatus";
        param.Value = showonstatus;
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
        param.ParameterName = "@bigimage";
        param.Value = bigimg;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@startdate";
        param.Value = sdate;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@enddate";
        param.Value = edate;
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


    public static bool insertchildimagepost(string productname, string productnumber, string shortdescription, char viewstatus, char showonstatus, string createdby, string bigimg, string sdate, string edate,int pid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insert_child_imagepost";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productname";
        param.Value = productname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@productnumber";
        param.Value = productnumber;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@shortdescription";
        param.Value = shortdescription;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@viewstatus";
        param.Value = viewstatus;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@showonstatus";
        param.Value = showonstatus;
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
        param.ParameterName = "@bigimage";
        param.Value = bigimg;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@startdate";
        param.Value = sdate;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@enddate";
        param.Value = edate;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = pid;
        param.DbType = DbType.Int32;
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


    public static bool addcategorytoproduct(int productid, int categoryid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertcategorytoproduct";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
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


    //sagar added below method for task 16dec2017
    public static bool addcategorytoproduct1(int productid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertcategorytoproduct1";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        //param = comm.CreateParameter();
        //param.ParameterName = "@categoryid";
        //param.Value = categoryid;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);
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


    public static DataTable getuserbypropductid(int productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "getuserbypid";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@pid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable replacestopword()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_replaceword";
        // create a new parameter
       
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static bool insertgrppost(int grpid, int pid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdate_grptopost";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@grpid";
        param.Value = grpid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@postid";
        param.Value = pid;
        param.DbType = DbType.Int32;
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


      public static DataTable getsinglesurveyuser(int sid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getsurveydetails";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@sid";
        param.Value = sid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        //Excute store procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
      public static bool updateevent(int productid, string productname, string shortdescription, string sdate, string edate)
      {
          // get a configured DbCommand object
          DbCommand comm = GenericDataAccess.CreateCommand();
          // set the stored procedure name
          comm.CommandText = "sp_proc_product_updateitem";
          // create a new parameter
          DbParameter param = comm.CreateParameter();
          param.ParameterName = "@productid";
          param.Value = productid;
          param.DbType = DbType.Int32;
          comm.Parameters.Add(param);

          // create a new parameter
          param = comm.CreateParameter();
          param.ParameterName = "@productname";
          param.Value = productname;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);

          // create a new parameter
          param = comm.CreateParameter();
          param.ParameterName = "@shortdescription";
          param.Value = shortdescription;
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

          int result = -1;
          // try
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
      public static bool deleteproduct(int productid)
      {
          // get a configured DbCommand object
          DbCommand comm = GenericDataAccess.CreateCommand();
          // set the stored procedure name
          comm.CommandText = "sp_proc_deleteproduct";
          // create a new parameter
          DbParameter param = comm.CreateParameter();
          param.ParameterName = "@productid";
          param.Value = productid;
          param.DbType = DbType.Int32;
          comm.Parameters.Add(param);

          int result = -1;

          result = GenericDataAccess.ExecuteNonQuery(comm);


          return (result != -1);
      }

      public static DataTable get_product_cattype(int pid)
      {
          // get a configured DbCommand object
          DbCommand comm = GenericDataAccess.CreateCommand();
          // set the stored procedure name
          comm.CommandText = "sp_proc_get_cattype_byproductid";
          // create a new parameter
          DbParameter param = comm.CreateParameter();
          param.ParameterName = "@productid";
          param.Value = pid;
          param.DbType = DbType.Int32;
          comm.Parameters.Add(param);
          //Excute store procedure
          return GenericDataAccess.ExecuteSelectCommand(comm);
      }

      public static DataTable getproductlikecountcomment(string pid)
      {
          // get a configured DbCommand object
          DbCommand comm = GenericDataAccess.CreateCommand();
          // set the stored procedure name
          comm.CommandText = "sp_proc_Getproductlikevisitcommentcount";
          // create a new parameter
          DbParameter param = comm.CreateParameter();
          param.ParameterName = "@pid";
          param.Value = pid;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);
          // execute the stored procedure
          return GenericDataAccess.ExecuteSelectCommand(comm);
      }

      public static DataTable deleteOldNotitication() // Delete Notitftaion where date is 2 month less then current date 
      {
          DbCommand comm = GenericDataAccess.CreateCommand();
          // set the stored procedure name
          comm.CommandText = "sp_proc_deleteAllNotification";
          // create a new parameter

          // execute the stored procedure
          return GenericDataAccess.ExecuteSelectCommand(comm);
      }

      public static bool insertupdate_video(string productname, string productnumber, string shortdescription, decimal ourprice, string smallimage, string bigimage, char viewstatus, char showonstatus, string createdby, string metatitle, string metakeyword, string metadescription, int pstock, decimal shiprate, char parentflag, string status, string year, int totalview, string parentchild, string file, string video, string videourl, string sdate, string edate)
      {
          // get a configured DbCommand object
          DbCommand comm = GenericDataAccess.CreateCommand();
          // set the stored procedure name
          comm.CommandText = "sp_proc_product_insertupdateitem_video";

          DbParameter param = comm.CreateParameter();
          param.ParameterName = "@productname";
          param.Value = productname;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);

          // create a new parameter
          param = comm.CreateParameter();
          param.ParameterName = "@productnumber";
          param.Value = productnumber;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);
          // create a new parameter
          param = comm.CreateParameter();
          param.ParameterName = "@shortdescription";
          param.Value = shortdescription;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);

          // create a new parameter
          param = comm.CreateParameter();
          param.ParameterName = "@ourprice";
          param.Value = ourprice;
          param.DbType = DbType.Decimal;
          comm.Parameters.Add(param);

          // create a new parameter
          param = comm.CreateParameter();
          param.ParameterName = "@smallimage";
          param.Value = smallimage;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);
          // create a new parameter
          param = comm.CreateParameter();
          param.ParameterName = "@bigimage";
          param.Value = bigimage;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);
          // create a new parameter
          param = comm.CreateParameter();
          param.ParameterName = "@viewstatus";
          param.Value = viewstatus;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);

          // create a new parameter
          param = comm.CreateParameter();
          param.ParameterName = "@showonstatus";
          param.Value = showonstatus;
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
          param.ParameterName = "@metatitle";
          param.Value = metatitle;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);
          // create a new parameter
          param = comm.CreateParameter();
          param.ParameterName = "@metakeyword";
          param.Value = metakeyword;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);
          // create a new parameter
          param = comm.CreateParameter();
          param.ParameterName = "@metadescription";
          param.Value = metadescription;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);

          // create a new parameter
          param = comm.CreateParameter();
          param.ParameterName = "@pstock";
          param.Value = pstock;
          param.DbType = DbType.Int32;
          comm.Parameters.Add(param);

          // create a new parameter
          param = comm.CreateParameter();
          param.ParameterName = "@shippingrate";
          param.Value = shiprate;
          param.DbType = DbType.Decimal;
          comm.Parameters.Add(param);

          // create a new parameter
          param = comm.CreateParameter();
          param.ParameterName = "@parentflag";
          param.Value = parentflag;
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
          param.ParameterName = "@year";
          param.Value = year;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);

          param = comm.CreateParameter();
          param.ParameterName = "@totalview";
          param.Value = totalview;
          param.DbType = DbType.Int32;
          comm.Parameters.Add(param);

          param = comm.CreateParameter();
          param.ParameterName = "@parentchild";
          param.Value = parentchild;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);

          param = comm.CreateParameter();
          param.ParameterName = "@filename";
          param.Value = file;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);

          param = comm.CreateParameter();
          param.ParameterName = "@videoembed";
          param.Value = video;
          param.DbType = DbType.String;
          comm.Parameters.Add(param);

          param = comm.CreateParameter();
          param.ParameterName = "@videourl";
          param.Value = videourl;
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

          int result = -1;
          result = GenericDataAccess.ExecuteNonQuery(comm);
          return (result >= 1);
      }
}
