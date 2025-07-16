using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Text.RegularExpressions;
public partial class IPAddressCheck : System.Web.UI.Page
{

    //[DllImport("Iphlpapi.dll")]
    //private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
    //[DllImport("Ws2_32.dll")]
    //private static extern Int32 inet_addr(string ip);
    #region CreativeMethods
    //SqlConnection source;
    //public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    #endregion

    SP_Methods spm = new SP_Methods();
    DataSet dspaymentVoucher_Apprs = new DataSet();
    String CEOInList = "N";
    double YearlymobileAmount = 0;

    [Obsolete]
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            var empCode = Convert.ToString(Session["Empcode"]);


            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }


            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }

            else
            {

                if (!Page.IsPostBack)
                {
                    string ipaddress;
                    ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (ipaddress == "" || ipaddress == null)
                        ipaddress = Request.ServerVariables["REMOTE_ADDR"];
                    //Response.Write("IP Address : " + ipaddress);
                    TextBox1.Text = ipaddress;
                    Page.SmartNavigation = true;

                   editform.Visible = true;
                   // hdnTaskRefID.Value = "0";
                   // var getVal = Convert.ToDouble(hdnTaskRefID.Value);
                   // ShowNetworkInterfaces();
                   // var IP1 = GetIPAddress();
                   // var IP2 = Get_IP1();
                   // var IP3 = Get_IP2();
                   // var IP4 = GetIPAddress_1();
                   // var IP5 = GetIPAddress_2();
                   // var IP6 = GetLocalIPAddress();
                   // var MACADDRESS = GetMACAddress2();
                   // var MACADDRESS1 = GetComputerName();
                    var VISITORIPADDRESS1 = VISITORIPADDRESS();
                   // //var sada_mac = GetClientMAC(GetIPAddress());
                  TextBox1.Text = "Client IP:-" + VISITORIPADDRESS1 ;
                   //TextBox1.Text = "Client IP:-" + VISITORIPADDRESS1 + "\nIP Address1.....  " + IP2 + "\nIP Address2.....  " + IP3 + "\nIP Address3.....  " + IP1 + "\nIP Address4:-" + IP4 + "\nIP Address5:-" + IP5 + "\nIP Address6:-" + IP6 + "\nIP Address7:-" + IP6;
                   //// TextBox1.Text = "IP Address1.....  " + IP2 + "\n\n\nMAC ADDRESS:-" + MACADDRESS + "\n\n\nCurrent IP Address:-" + MACADDRESS1;
                }
                else
                {

                }
            }


        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }
    ////public void ShowNetworkInterfaces()
    ////{
    ////    var g = "";
    ////    foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
    ////    {
    ////        if (ni.OperationalStatus != OperationalStatus.Up)
    ////        {
    ////            // The interface is disabled; skip it.
    ////            continue;
    ////        }

    ////        Console.WriteLine(ni.Name);
    ////        IPInterfaceProperties ipProp = ni.GetIPProperties();
    ////        foreach (UnicastIPAddressInformation ip in ipProp.UnicastAddresses)
    ////        {
    ////            if (ip.Address.AddressFamily != AddressFamily.InterNetwork)
    ////            {
    ////                // The IP address is not an IPv4 address.
    ////                continue;
    ////            }
    ////            g += "<br>Privte  IP Address........." + ip.Address;
    ////            g += "</br>Subnet Mask................" + ip.IPv4Mask;

    ////            break;
    ////        }

    ////        foreach (GatewayIPAddressInformation gateway in ipProp.GatewayAddresses)
    ////        {
    ////            if (gateway.Address.AddressFamily != AddressFamily.InterNetwork)
    ////            {
    ////                // The gateway address is not an IPv4 address.
    ////                continue;
    ////            }
    ////            g += "</br>Default Gateway..........." + gateway.Address;
    ////            // Console.WriteLine("Default gateway: {0}", gateway.Address);
    ////            // break;
    ////        }

    ////        //Console.WriteLine("DNS servers:");
    ////        g += "</br></br> DNS Servers";
    ////        foreach (IPAddress ip in ipProp.DnsAddresses)
    ////            g += " </br> Public IP Address........." + ip;
    ////        break;
    ////    }
    ////    IPHostEntry host;
    ////    host = Dns.GetHostEntry(Dns.GetHostName());

    ////    foreach (IPAddress ip in host.AddressList)
    ////    {
    ////        if (ip.AddressFamily.ToString() == "InterNetwork")
    ////        {
    ////            g+= "</br> New IP Address:-" + ip;
    ////        }
    ////    }
    ////    string localIP;
    ////    using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
    ////    {
    ////        socket.Connect("8.8.8.8", 65530);
    ////        IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
    ////        localIP = endPoint.Address.ToString();
    ////        g += "</br>New IP Address 1:-" + localIP;
    ////    }
    ////    // lblmessage.Text = g;
    ////    // TextBox1.Text = g;
    ////    Label2.Text = g;
    ////}

    ////public string GetIPAddress_mac()
    ////{
    ////    System.Web.HttpContext context = System.Web.HttpContext.Current;
    ////    string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

    ////    if (!string.IsNullOrEmpty(ipAddress))
    ////    {
    ////        string[] addresses = ipAddress.Split(',');
    ////        if (addresses.Length != 0)
    ////        {
    ////            return addresses[0];
    ////        }
    ////    }

    ////    return context.Request.ServerVariables["REMOTE_ADDR"];
    ////}

    ////public static string GetClientMAC(string strClientIP)
    ////{
    ////    string mac_dest = "";
    ////    try
    ////    {
    ////        Int32 ldest = inet_addr(strClientIP);
    ////        Int32 lhost = inet_addr("");
    ////        Int64 macinfo = new Int64();
    ////        Int32 len = 6;
    ////        int res = SendARP(ldest, 0, ref macinfo, ref len);
    ////        string mac_src = macinfo.ToString("X");

    ////        while (mac_src.Length < 12)
    ////        {
    ////            mac_src = mac_src.Insert(0, "0");
    ////        }

    ////        for (int i = 0; i < 11; i++)
    ////        {
    ////            if (0 == (i % 2))
    ////            {
    ////                if (i == 10)
    ////                {
    ////                    mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
    ////                }
    ////                else
    ////                {
    ////                    mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
    ////                }
    ////            }
    ////        }
    ////    }
    ////    catch (Exception err)
    ////    {
    ////        throw new Exception("L?i " + err.Message);
    ////    }
    ////    return mac_dest;
    ////}


    //public string GetIPAddress()
    //{
    //    string ip = "";
    //    try
    //    {
    //        string _ipList = "";
    //        //string _ipList = HttpContext.Current.Request.Headers["CF-CONNECTING-IP"].ToString();

    //        _ipList = HttpContext.Current.Request.ServerVariables["HTTP_X_CLUSTER_CLIENT_IP"];
    //        if (!string.IsNullOrWhiteSpace(_ipList))
    //        {
    //            ip += _ipList.Split(',')[0].Trim();
    //        }
    //        else
    //        {
    //            _ipList = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
    //            if (!string.IsNullOrWhiteSpace(_ipList))
    //            {
    //                ip += _ipList.Split(',')[0].Trim();
    //            }
    //            else
    //            {
    //                ip += HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString().Trim();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ip += ex.ToString();
    //    }
    //    return ip;
    //}
    //////[DllImport("iphlpapi.dll", CharSet = CharSet.Auto)]
    //////private static extern int GetBestInterface(UInt32 destAddr, out UInt32 bestIfIndex);
    ////public static IPAddress GetDefaultGateway1()
    ////{
    ////    return NetworkInterface
    ////        .GetAllNetworkInterfaces()
    ////        .Where(n => n.OperationalStatus == OperationalStatus.Up)
    ////        .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
    ////        .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
    ////        .Select(g => g?.Address)
    ////        .Where(a => a != null)
    ////        // .Where(a => a.AddressFamily == AddressFamily.InterNetwork)
    ////        // .Where(a => Array.FindIndex(a.GetAddressBytes(), b => b != 0) >= 0)
    ////        .FirstOrDefault();
    ////}
    //public static IPAddress GetDefaultGateway()
    //{
    //    var gateway_address = NetworkInterface.GetAllNetworkInterfaces()
    //        .Where(e => e.OperationalStatus == OperationalStatus.Up)
    //        .SelectMany(e => e.GetIPProperties().GatewayAddresses)
    //        .FirstOrDefault();
    //    if (gateway_address == null) return null;
    //    return gateway_address.Address;
    //}
    //public string Get_IP1()
    //{
    //    var localIP = "";
    //    System.Web.HttpContext context = System.Web.HttpContext.Current;
    //    string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
    //    ipAddress += "HTTP_X_COMING_FROM:-" + context.Request.ServerVariables["HTTP_X_COMING_FROM"];
    //    ipAddress +="1:-"+ context.Request.ServerVariables["HTTP_X_FORWARDED"];
    //    ipAddress += "2:-" + context.Request.ServerVariables["HTTP_X_REAL_IP"];
    //    ipAddress += "3:-" + context.Request.ServerVariables["HTTP_VIA"];
    //    ipAddress += "4:-" + context.Request.ServerVariables["HTTP_COMING_FROM"];
    //    ipAddress += "5:-" + context.Request.ServerVariables["HTTP_FORWARDED_FOR"];
    //    ipAddress += "6:-" + context.Request.ServerVariables["HTTP_FORWARDED"];
    //    ipAddress += "7:-" + context.Request.ServerVariables["HTTP_FROM"];
    //    ipAddress += "8:-" + context.Request.ServerVariables["HTTP_PROXY_CONNECTION"];
    //    ipAddress += "9:-" + context.Request.ServerVariables["CLIENT_IP"];
    //    ipAddress += "10:-" + context.Request.ServerVariables["FORWARDED"];

    //    if (!string.IsNullOrEmpty(ipAddress))
    //    {
    //        string[] addresses = ipAddress.Split(',');
    //        if (addresses.Length != 0)
    //        {
    //            return addresses[0];
    //        }
    //    }

    //    return context.Request.ServerVariables["REMOTE_ADDR"];
    //    // return localIP;

    //}

    //// [Obsolete]
    //public string Get_IP2()
    //{

    //    string strHostName = "";
    //    //strHostName = System.Net.Dns.GetHostName();

    //    //var t = GetIPAddress(Dns.GetHostName());
    //    //IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

    //    //var addr = t.AddressFamily;

    //    //return addr.ToString();
    //    string IP = HttpContext.Current.Request.Params["HTTP_CLIENT_IP"] ?? HttpContext.Current.Request.UserHostAddress;


    //    return IP;
    //}
    //public static IPAddress GetIPAddress(string hostName)
    //{
    //    Ping ping = new Ping();
    //    var replay = ping.Send(hostName);

    //    if (replay.Status == IPStatus.Success)
    //    {
    //        return replay.Address;
    //    }
    //    return null;
    //}
    //////public static IEnumerable<string> GetAddresses()
    //////{
    //////    var host = Dns.GetHostEntry(Dns.GetHostName());
    //////    return (from ip in host.AddressList where ip.AddressFamily == AddressFamily.InterNetwork select ip.ToString()).ToList();
    //////}
    ////private void BindMyTaskGrid(DataTable dataTable)
    ////{
    ////    try
    ////    {

    ////        gv_MyTask.DataSource = null;
    ////        gv_MyTask.DataBind();
    ////        if (dataTable.Rows.Count > 0)
    ////        {
    ////            gv_MyTask.DataSource = dataTable;
    ////            gv_MyTask.DataBind();
    ////        }
    ////        else
    ////        {

    ////        }
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        lblmessage.Text = ex.Message.ToString();
    ////    }
    ////}


    //protected void MyTask_Edit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        LinkButton btn = (LinkButton)sender;
    //        var MyTask_EditVal = Convert.ToDouble(btn.CommandArgument.Trim());
    //        // Response.Redirect("~/procs/TaskMonitoring.aspx");
    //        Response.Redirect("~/procs/Update_Task.aspx?id=" + MyTask_EditVal);
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }



    //}

    //protected string GetIPAddress_1()
    //{
    //    List<string> ips = new List<string>();

    //    System.Net.IPHostEntry entry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());

    //    foreach (System.Net.IPAddress ip in entry.AddressList)
    //        if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
    //            ips.Add(ip.ToString());

    //    return ips[ips.Count - 1];
    //}
    //public static void write2log(string strmsg)
    //{

    //    System.IO.StreamWriter sw = new System.IO.StreamWriter(System.Configuration.ConfigurationSettings.AppSettings["LogTestPath"] +
    //         "Log_" + DateTime.Now.Day.ToString() + ".txt", true);
    //    sw.WriteLine(strmsg);
    //    sw.Flush();
    //    sw.Close();
    //}
    //protected string GetIPAddress_2()
    //{
    //    //string ip = Request.UserHostAddress;
    //    string ip = HttpContext.Current.Request.UserHostAddress; ;
    //    var IPAddress = "";
    //    try
    //    {
    //        // Get request.
    //        HttpRequest request = base.Request;

    //        // Get UserHostAddress property.
    //        string address = request.UserHostAddress;
    //        IPAddress = address;

    //    }
    //    catch (Exception ex)
    //    { }
    //    return IPAddress;
    //}

    //public string GetLocalIPAddress()
    //{
    //    var host = Dns.GetHostEntry(Dns.GetHostName());
    //    foreach (var ip in host.AddressList)
    //    {
    //        if (ip.AddressFamily == AddressFamily.InterNetwork)
    //        {
    //            return ip.ToString();
    //        }
    //    }
    //    throw new Exception("No network adapters with an IPv4 address in the system!");
    //}
    ////public static string GetMACAddress1()
    ////{
    ////    System.Management.ManagementObjectSearcher objMOS = new System.Management.ManagementObjectSearcher("Select * FROM Win32_NetworkAdapterConfiguration");
    ////    System.Management.ManagementObjectCollection objMOC = objMOS.Get();
    ////    string macAddress = String.Empty;
    ////    foreach (System.Management.ManagementObject objMO in objMOC)
    ////    {
    ////        object tempMacAddrObj = objMO["MacAddress"];

    ////        if (tempMacAddrObj == null) //Skip objects without a MACAddress
    ////        {
    ////            continue;
    ////        }
    ////        if (macAddress == String.Empty) // only return MAC Address from first card that has a MAC Address
    ////        {
    ////            macAddress = tempMacAddrObj.ToString();
    ////        }
    ////        objMO.Dispose();
    ////    }
    ////    // macAddress = macAddress.Replace(":", "");
    ////    // macAddress = Regex.Replace(macAddress, @"\w{2}(?=[^-])", "$0-"); 
    ////    return macAddress;
    ////}

    //public static string GetMACAddress2()
    //{
    //    string addr = "";
    //    foreach (NetworkInterface n in NetworkInterface.GetAllNetworkInterfaces())
    //    {
    //        if (n.OperationalStatus == OperationalStatus.Up)
    //        {
    //            addr += n.GetPhysicalAddress().ToString();
    //            var add = Regex.Replace(addr, @"\w{2}(?=[^-])", "$0-");
    //            addr = add;
    //            //addr += n.GetIPv4Statistics().ToString();
    //            // addr += n.
    //            break;
    //        }
    //    }
    //    return addr;
    //}
    //public string GetComputerName()
    //{
    //    try
    //    {
    //        List<string> ips = new List<string>();

    //        System.Net.IPHostEntry entry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());

    //        foreach (System.Net.IPAddress ip in entry.AddressList)
    //            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
    //                ips.Add(ip.ToString());

    //        return ips[ips.Count - 1];
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //    return "";
    //}
    //public string VISITORIPADDRESS()
    //{
    //    var returnString = "";
    //    var VisitorsIPAddress = Page.Request.UserHostAddress;


    //    //try
    //    //{
    //    //    if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
    //    //    {
    //    //        VisitorsIPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
    //    //    }
    //    //    else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
    //    //    {
    //    //        VisitorsIPAddress = HttpContext.Current.Request.UserHostAddress;
    //    //    }
    //    //}
    //    //catch (Exception ex)
    //    //{

    //    //}
    //    returnString = VisitorsIPAddress+" Test";
    //    return returnString;
    //}

    public string VISITORIPADDRESS()
    {
        var returnString = "";
        var VisitorsIPAddress = Page.Request.UserHostAddress;


        try
        {
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddress = HttpContext.Current.Request.UserHostAddress;
            }
        }
        catch (Exception ex)
        {

        }
           var SesstionId = HttpContext.Current.Session.SessionID;
        var resultString = Regex.Replace(SesstionId, "[^0-9]+", string.Empty);
        returnString = VisitorsIPAddress+ "\nSessionID:-" + HttpContext.Current.Session.SessionID+"\nSesstion Id1:-"+ resultString;
        return returnString;
    }
}