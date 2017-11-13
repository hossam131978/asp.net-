using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddManagers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {  if(!IsPostBack)
        //ButtonSubmit.Enabled = false;
        ErrorLabel.Visible = false;
    }
    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        localhost.WebServiceOperatingSystem ws = new localhost.WebServiceOperatingSystem();
        int id = 0;
        int added = 0;
        if (int.TryParse(TextBoxId.Text, out id))
            added = ws.AddManager(TextBoxUserName.Text, TextBoxPassword.Text, id, TextBoxName.Text, TextBoxField.Text);
        if (added == 1)
        {
            ErrorLabel.Visible = true;
            ErrorLabel.Text = "the manager : '" + TextBoxName.Text + "'  is added successfully";
            ErrorLabel.BackColor = System.Drawing.Color.Green;

        }
        if (added == -1)
        {
            ErrorLabel.Visible = true;
            ErrorLabel.Text = "'the id is exists'   the manager : '" + TextBoxName.Text + "'       was not added ";
            ErrorLabel.BackColor = System.Drawing.Color.Red;

        }
        if (added == 0)
        {
            ErrorLabel.Visible = true;
            ErrorLabel.Text = "'the data is invalid' the manager : '" + TextBoxName.Text + "'  was not added ";
            ErrorLabel.BackColor = System.Drawing.Color.Red;

        }
        if (added == 111)
        {
            ErrorLabel.Visible = true;
            ErrorLabel.Text = "'error in the connection' the manager : '" + TextBoxName.Text + "'  was not added ";
            ErrorLabel.BackColor = System.Drawing.Color.Red;

        }
        if (added == 112)
        {
            ErrorLabel.Visible = true;
            ErrorLabel.Text = "'the user name is exists' the manager : '" + TextBoxName.Text + "'  was not added ";
            ErrorLabel.BackColor = System.Drawing.Color.Red;

        }

    }
}