using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnumTasks;


public partial class WebFormPages_Managers_AddTasks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ButtonSubmit.Enabled = false;
        if (!IsPostBack)
        {
            ErrorLabel.Visible = false;
            localhost.WebServiceOperatingSystem ws = new localhost.WebServiceOperatingSystem();
            if (Session["UserName"] != null && Session["Password"] != null && Session["Id"] != null)
            {
                
                System.Data.DataTable dt = new System.Data.DataTable();
                try
                {
                 dt=   ws.GetManagerEmployeesId(Session["UserName"].ToString(), Session["Password"].ToString(), int.Parse(Session["Id"].ToString()));
                    DropDownListEmployeeId.DataSource = dt;
                    DropDownListEmployeeId.DataTextField=dt.Columns[0].ToString();
                    DropDownListEmployeeId.DataValueField = dt.Columns[0].ToString();
                    DropDownListEmployeeId.DataBind();
                    List<int> priority=new List<int>();
                    foreach (var item in Enum.GetValues(typeof(EnumTaskPriority)))
                    {
                        priority.Add((int)item);
                    }
                    DropDownListTaskPriority.DataSource = priority;
                    DropDownListTaskPriority.DataBind();
                         
                }
                catch { Response.Redirect(@"~/Login.aspx"); }
            }
            else Response.Redirect(@"~/Login.aspx");
        }
    }
    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        localhost.WebServiceOperatingSystem ws = new localhost.WebServiceOperatingSystem();
        int TaskId = 0;
        int ManagerId = 0;
        int EmployeeId = 0;
        string TaskDescription=TextBoxTaskDescription.Text;
        int FulfillmentTime = 0;
        int TaskPriority = 0;

        int added = 0;

        if (int.TryParse(TextBoxTaskId.Text, out TaskId) && int.TryParse(DropDownListEmployeeId.SelectedValue, out EmployeeId) && int.TryParse(TextBoxTaskFulfillmentTime.Text, out FulfillmentTime) &&
              int.TryParse(DropDownListTaskPriority.SelectedValue, out TaskPriority) && int.TryParse(Session["id"].ToString(), out ManagerId))
            added = ws.AddTask(TaskId, ManagerId, EmployeeId, TaskDescription, FulfillmentTime, TaskPriority);
        if (added == 1)
        {
            ErrorLabel.Visible = true;
            ErrorLabel.Text = "the Task : '" + TaskId + "'  is added successfully";
            ErrorLabel.BackColor = System.Drawing.Color.Green;

        }
        if (added == -1)
        {
            ErrorLabel.Visible = true;
            ErrorLabel.Text = "'the id is exists'   the Task : '" + TaskId + "'       was not added ";
            ErrorLabel.BackColor = System.Drawing.Color.Red;

        }
        if (added == 0)
        {
            ErrorLabel.Visible = true;
            ErrorLabel.Text = "'the data is invalid' the Task : '" + TaskId + "'  was not added ";
            ErrorLabel.BackColor = System.Drawing.Color.Red;

        }
        if (added == 111)
        {
            ErrorLabel.Visible = true;
            ErrorLabel.Text = "'error in the connection' the manager : '" + TaskId + "'  was not added ";
            ErrorLabel.BackColor = System.Drawing.Color.Red;

        }
        if (added == 112)
        {
            ErrorLabel.Visible = true;
            ErrorLabel.Text = "'error in server : '" + ws.Get_sql_exeption()+ "'    +the Task : '" + TaskId +  "'  was not added ";
            ErrorLabel.BackColor = System.Drawing.Color.Red;

        }

 
    }
}