using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
/// <summary>
/// Summary description for classGetAttrProducts
/// </summary>
public class classGetAttrProducts
{
	public classGetAttrProducts()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataTable  getGroupAttributes(int productId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_GetAttrGrpProducts";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@pProductId";
        param.Value = productId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        return dt;
    }
    public static DataTable getGroupAttributescolor(int productId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_GetAttrGrpProductscolor";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@pProductId";
        param.Value = productId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        return dt;
    }
    public static DataTable getProductAttributes(string  productid, string AttributeGroupId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
       // comm.CommandText = "usp_getattributerelatedtoproductbygAttrid";
        //comm.CommandText = "usp_getattributerelatedtoproductbygAttrgrid";
        comm.CommandText = "usp_getattributerelatedtoproductbygAttrgrid1";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@AttributeGroupId";
        param.Value = AttributeGroupId;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        return dt;
    }

    public static DataSet getProdNewID(string prodId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_GetSelectedProductIDAttrs";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@pAttrId";
        param.Value = prodId;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCmd(comm);
    }

  
    public static DataTable getcommaseparedattributeid(string attrigrpid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
      
        comm.CommandText = "sp_getattributeidwithcomma";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@strattrgrid";
        param.Value = attrigrpid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getprodgridbyprodid(string attrigrpid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();

        comm.CommandText = "sp_getattributegrpbyproductid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@strprdid";
        param.Value = attrigrpid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    //new method on 26 may 
    public static DataTable getprodattridbyprodid_attrgpid(string attrigrpid,string productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();

        comm.CommandText = "sp_getattributeidbyproductattrgrpid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@strattrid";
        param.Value = attrigrpid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@strprdid";
        param.Value = productid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    //new method on 26 may 
    public static DataTable getprodattridbyprodid_variant(string attrigrpid, string productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();

        comm.CommandText = "sp_getattributeidbyproductattrgrpid_variant";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@strattrid";
        param.Value = attrigrpid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@strprdid";
        param.Value = productid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
}