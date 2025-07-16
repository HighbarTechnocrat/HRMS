using System;
using System.Collections.Generic;

public partial class chatgptlogin : System.Web.UI.Page
{
    // Mock user data
    private static readonly Dictionary<string, string> users = new Dictionary<string, string>
    {
        { "sanjay.patil@highbartech.com", "password123" }
    };

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text.Trim();
        string password = txtPassword.Text.Trim();

        if (users.ContainsKey(email) && users[email] == password)
        {
            lblMessage.Text = "Login successful!";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            // You can redirect the user to another page, e.g.:
            // Response.Redirect("HomePage.aspx");
        }
        else
        {
            lblMessage.Text = "Invalid credentials. Please try again.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
}
