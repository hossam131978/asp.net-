using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {  
        ErrorLabel.Visible = false;
    }
    protected void Button_login_click(object sender, EventArgs e)
    {

        if (!String.IsNullOrWhiteSpace(TextBoxName.Text) && !String.IsNullOrWhiteSpace(TextBoxPassword.Text))
        {
            string str;
            int check = 0;
            localhost.WebServiceOperatingSystem ws;
            try
            {
                ws = new localhost.WebServiceOperatingSystem();
                str = ws.CheckPassword(TextBoxName.Text, TextBoxPassword.Text);

            }
            catch (Exception ex)
            {

                ErrorLabel.Visible = true;
                ErrorLabel.Text = ex.Message;
                Button_login.Enabled = false;
                return;
            }

            var id_name_check = str.Split(',');
            check = int.Parse(id_name_check[2]);
            if (check == 0)
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "connection error";
                //Button_login.BackColor = System.Drawing.Color.Red ;
                //Button_login.Attributes.CssStyle[HtmlTextWriterStyle.Color] = System.Drawing.Color.Black.ToString();

                Button_login.Enabled = false;
                return;

            }
            if (check == 111)
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "connection error 'check connection string'";
                Button_login.Enabled = false;
                return;
            } if (check == 112)
            {
                ErrorLabel.Visible = true;
          //      ErrorLabel.Text = ws.sql_exeption;
                Button_login.Enabled = false;
                return;
            }
            if (check == -1)
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "the password or the user name is wrong";
                Button_login.Enabled = false;
                return;
            }
            Session["Id"] = int.Parse(id_name_check[0]);
            Session["Name"] = id_name_check[1].ToString();
            Session["UserName"] = TextBoxName.Text;
            Session["Password"] = TextBoxPassword.Text;
            if (check == 3)
            {
                Response.Redirect(@"~/WebFormPages/HeadMaster/HeadMaster.aspx");
            }

            if (check == 2)
            {
                Response.Redirect(@"~/WebFormPages/Managers/Managers.aspx");
            }
            if (check == 1)
            {
                Response.Redirect(@"~/WebFormPages/Employees/Employee.aspx");
            }
        }

    }

   
}