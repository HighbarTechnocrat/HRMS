using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_LocalIPAddress : System.Web.UI.Page
{
    string ipAdd = "";
    string userip = "";
    protected void Page_Load(object sender, EventArgs e)
    {

       

        //GetIpValue(out ipAdd);
        GetIpAddress(out userip);

    }

    //private void GetIpValue(out string ipAdd)
    //{
    //    ipAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

    //    if (string.IsNullOrEmpty(ipAdd))
    //    {
    //        ipAdd = Request.ServerVariables["REMOTE_ADDR"];
    //    }
    //    else
    //    {
    //        LBlIPv4.Text = ipAdd;
    //    }
    //}

    private void GetIpAddress(out string userip)
    {
        userip = Request.UserHostAddress;
        if (Request.UserHostAddress != null)
        {
            Int64 macinfo = new Int64();
            string macSrc = macinfo.ToString("X");
            if (macSrc == "0")
            {
                if (userip == "127.0.0.1")
                {
                    Response.Write("visited Localhost!");
                }
                else
                {
                    TextBox1.Text = userip;
                }
            }
        }
    }




}