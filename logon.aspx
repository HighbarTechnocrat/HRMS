<%@ Page language="c#" AutoEventWireup="true" %>
<%@ Import Namespace="FormsAuth" %>
<%@ Import Namespace="System.Security.Principal" %>
<html>
  <body>	
    <form id="Login" method="post" runat="server">
      <asp:Label ID="Label1" Runat="server" Visible="false">Domain:</asp:Label>
      <asp:TextBox ID="txtDomain" Runat="server" Visible="false" Text="lavasa.net" ></asp:TextBox><br>    
      <asp:Label ID="Label2" Runat="server" >Username:</asp:Label>
      <asp:TextBox ID="txtUsername" Runat="server" ></asp:TextBox><br>
      <asp:Label ID="Label3" Runat="server" >Password:</asp:Label>
      <asp:TextBox ID="txtPassword" Runat="server" TextMode="Password"></asp:TextBox><br>
      <asp:Button ID="btnLogin" Runat="server" Text="Login" OnClick="Login_Click"></asp:Button><br>
      <asp:Label ID="errorLabel" Runat="server"></asp:Label><br>
      <asp:CheckBox ID="chkPersist" Runat="server" Text="Persist Cookie" />
    </form>	
  </body>
</html>
<script runat="server">
void Login_Click(Object sender, EventArgs e)
{

    //String adPath = "LDAP://lavasa.net"; //Fully-qualified Domain Name
    String adPath = "LDAP://highbartech.com"; //Fully-qualified Domain Name
  LdapAuthentication adAuth = new LdapAuthentication(adPath);
  try
  {
      IPrincipal currUser;
      
      currUser = HttpContext.Current.User;
      
     // txtDomain.Text = "lavasa.net";
      txtDomain.Text = "highbartech.com";
     // txtUsername.Text = "test1";
     //txtPassword.Text = "INDIA$1234";
      
    //if(true == adAuth.IsAuthenticated(txtDomain.Text, txtUsername.Text, txtPassword.Text))
   if ("true" == Convert.ToString(adAuth.IsAuthenticated(txtDomain.Text, txtUsername.Text, txtPassword.Text)))
    {
      String groups="Highbartech"; //= adAuth.GetGroups();

     /* //Create the ticket, and add the groups.
      bool isCookiePersistent = chkPersist.Checked;
      FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1,  txtUsername.Text,
      DateTime.Now, DateTime.Now.AddMinutes(60), isCookiePersistent, groups);
	
      //Encrypt the ticket.
      String encryptedTicket = FormsAuthentication.Encrypt(authTicket);
		
      //Create a cookie, and then add the encrypted ticket to the cookie as data.
      HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

      if(true == isCookiePersistent)
	  authCookie.Expires = authTicket.Expiration;
				
      //Add the cookie to the outgoing cookies collection.
      Response.Cookies.Add(authCookie);		

      //You can redirect now.
      //Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUsername.Text, false));
      */
    //  Response.Redirect("webform1.aspx");
      //Response.Redirect("http://mysteinerindia?uloginid=" + txtUsername.Text, false);
      //Response.Redirect("http://www.highbartech.com");
	  Response.Redirect("http://localhost/hrms/default.aspx");
      
    }
    else
    {
      errorLabel.Text = "Authentication did not succeed. Check user name and password.";
    }
  }
  catch(Exception ex)
  {
    errorLabel.Text = "Error authenticating. " + ex.Message;
  }
}
</script>