using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Configuration;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class OrgService
{
	// To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
	// To create an operation that returns XML,
	//     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
	//     and include the following line in the operation body:
	//         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
	[OperationContract]
    [WebInvoke(UriTemplate = "/", ResponseFormat = WebMessageFormat.Json)]
    public string GetEmployee()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconnection"].ToString());
            if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
                con.Open();
            SqlCommand sCommand = new SqlCommand("GetEmployeesOrg", con);
            //sCommand.Connection = con;
            sCommand.CommandTimeout = 0;
            //sCommand.CommandText = "Usp_getEmployee_Details_All";
            sCommand.CommandType = CommandType.StoredProcedure;

            //SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
            //SqlCommand command = new SqlCommand("json", con);
            //command.CommandType = CommandType.StoredProcedure;
            //con.Open();
            return (string)sCommand.ExecuteScalar();
        }
        catch (SqlException ex)
        {
            return ex.Message.ToString();

        }
    }
	// Add more operations here and mark them with [OperationContract]
}
