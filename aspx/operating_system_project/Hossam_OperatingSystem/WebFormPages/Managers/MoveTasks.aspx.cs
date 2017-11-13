using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebFormPages_Managers_MoveTasks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            DropDownListTaskId.SelectedIndex = 0;
            ErrorLabel.Visible = false;
            GetData();
        }
    }

    //fill the drop down lists by manager id
    protected void GetData()
    {
        if (Session["UserName"] != null && Session["Password"] != null && Session["Id"] != null)
        {
              if(Session["destination_index"] !=null && Session["source_index"] != null)
              {
                  DropDownListDestinationEmployeeId.SelectedIndex = int.Parse(Session["destination_index"].ToString());
                  DropDownListTaskId.SelectedIndex = int.Parse(Session["source_index"].ToString());
              }

            try
            {
                localhost.WebServiceOperatingSystem ws = new localhost.WebServiceOperatingSystem();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = ws.GetManagerEmployeesId(Session["UserName"].ToString(), Session["Password"].ToString(), (int)Session["Id"]);
                DropDownListDestinationEmployeeId.DataSource = dt;
                DropDownListDestinationEmployeeId.DataTextField = dt.Columns[0].ToString();
                DropDownListDestinationEmployeeId.DataBind();
                GridView1.DataSource = dt;
                GridView1.DataBind();
                
            }
            catch (Exception) { Response.Redirect(@"~/Login.aspx"); }


            try
            {
                localhost.WebServiceOperatingSystem ws = new localhost.WebServiceOperatingSystem();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = ws.GetTasksIdsAndEmployeesIds(int.Parse(Session["Id"].ToString()));
                DropDownListTaskId.DataSource = dt;
                DropDownListTaskId.DataTextField = dt.Columns[0].ToString();
                DropDownListTaskId.DataBind();
                
                DropDownListTaskEmployeesId.DataSource = dt;
                DropDownListTaskEmployeesId.DataTextField = dt.Columns[1].ToString();
                DropDownListTaskEmployeesId.DataBind();
                labelSourceEmployeeId.Text = DropDownListTaskEmployeesId.Items[DropDownListTaskId.SelectedIndex].Value;
                
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception) { Response.Redirect(@"~/Login.aspx"); }

        }
        else Response.Redirect(@"~/Login.aspx");
    }


    protected void ButtonMove_Click(object sender, EventArgs e)
    {
        int TaskId = 0;
        int index = DropDownListTaskId.SelectedIndex;
            TaskId = int.Parse(DropDownListTaskId.Items[DropDownListTaskId.SelectedIndex].Text.ToString());
        localhost.WebServiceOperatingSystem ws = new localhost.WebServiceOperatingSystem();
        int EmployeeId = 0;

        int added = 0;
        try
        {
            TaskId = int.Parse(DropDownListTaskId.Items[DropDownListTaskId.SelectedIndex].Text.ToString());

        }
        catch (Exception ex)
        {
        }
        try
        {
            EmployeeId = int.Parse(DropDownListDestinationEmployeeId.Items[DropDownListDestinationEmployeeId.SelectedIndex].Text.ToString());
        }
        catch (Exception ex)
        {
        }

        {

                added = ws.MoveTask(TaskId, EmployeeId);
            if (added == 1)
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "the Task : '" + TaskId + "'  is moved successfully";
                ErrorLabel.BackColor = System.Drawing.Color.Green;
                Session["destination_index"] = DropDownListDestinationEmployeeId.SelectedIndex;
                Session["source_index"] = DropDownListTaskId.SelectedIndex;

                GetData();

            }
            if (added == -1)
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "'the id is exists'   the Task : '" + TaskId + "'       was not moved ";
                ErrorLabel.BackColor = System.Drawing.Color.Red;

            }
            if (added == 0)
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "'the data is invalid' the Task : '" + TaskId + "'  was not moved ";
                ErrorLabel.BackColor = System.Drawing.Color.Red;

            }
            if (added == 111)
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "'connection error' the Task : '" + TaskId + "'   was not moved ";
                ErrorLabel.BackColor = System.Drawing.Color.Red;

            }
        }
    }
    
}