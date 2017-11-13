using EnumTasks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//Session["method"]   for GetTaskStatus_Or_TaskPriority_List method must be PropertiesDropDownList_enum type
//Session["gridview"]   saves the last method that fill's the grid view  (the data is tasks or employees......)
//
public partial class Employee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        labelError.Visible = false;
        if (Session["UserName"] != null && Session["Password"] != null)
        {
            if (!IsPostBack)
            {
                Session["gridview"] = "GetNewTasks";
                GetNewTasks();
                UserName_Label.Text = "Welcome   " + Session["Name"].ToString();
            }
        }
        else Response.Redirect(@"~/Login.aspx");
    }

    //  fill the cache["table"] with the relevant date table
    protected void FillCache()
    {
        {
            try
            {
                localhost.WebServiceOperatingSystem ws = new localhost.WebServiceOperatingSystem();
                Cache["table"] = ws.GetTasksByEmployeeId(Session["UserName"].ToString(), Session["Password"].ToString(), (int)Session["Id"]);
            }
            catch (Exception ex)
            {
                labelError.Visible = true;
                labelError.ForeColor = System.Drawing.Color.Red;
                labelError.Text = ex.Message;
            }
        }
    }


    //sort the grid view by selected column
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            //Sort the data.


            DataTable ta = new DataTable();
            if (Session["gridview"] == "GetNewTasks")
                ta = GetNewTasks();
            if (Session["gridview"] == "GetAcceptedTasks")
                ta = GetAcceptedTasks();

            //Sort the data.
            ta.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
            GridView1.DataSource = ta;
            GridView1.DataBind();
            DetailsView1.DefaultMode = DetailsViewMode.ReadOnly;
            BindDetailsView1();
        }
        catch (Exception ex)
        {
            labelError.Visible = true;
            labelError.ForeColor = System.Drawing.Color.Red;
            labelError.Text = ex.Message;
        }

    }
    //dec or  asci sorting
    protected string GetSortDirection(string column)
    {

        // By default, set the sort direction to ascending.
        string sortDirection = "ASC";

        // Retrieve the last column that was sorted.
        string sortExpression = Cache["SortExpression"] as string;

        if (sortExpression != null)
        {
            // Check if the same column is being sorted.
            // Otherwise, the default value can be returned.
            if (sortExpression == column)
            {
                string lastDirection = Cache["SortDirection"] as string;
                if ((lastDirection != null) && (lastDirection == "ASC"))
                {
                    sortDirection = "DESC";
                }
            }
        }

        // Save new values in Cache.
        Cache["SortDirection"] = sortDirection;
        Cache["SortExpression"] = column;

        return sortDirection;
    }

    //select the selected row and show data with details view to edit and update
    protected void GridView1_SelectedIndexChanging(object sender, EventArgs e)
    {

        FillCache();//check if the cache is empty then fill it
        DetailsView1.ChangeMode(DetailsViewMode.ReadOnly); //returns the mode to read only if it was updating?
        BindDetailsView1();
    }

    private void BindDetailsView1()  //binds the data(new tasks or finished tasks) to the DetailsView 
    {
        try  //for the tasks that need permission(new tasks)
        {
            if (GridView1.SelectedIndex >= 0)
            {
                var Id = GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text; //saves the id for the table sorting "because details view not supporting sorting"
                if (Session["gridview"] == "GetNewTasks")
                {
                    FillCache();

                    //fill the details view with the selected row data from grid view
                    string exp = string.Format("TaskId={0}", Id);  //the sort expretion 
                    var gridview1_row = (Cache["table"] as DataTable).Select(exp);

                    DetailsView1.DataSource = gridview1_row.CopyToDataTable<DataRow>();
                    DetailsView1.DataBind();

                    DetailsView1.Rows[0].Enabled = false;
                    DetailsView1.Rows[1].Visible = false;
                    DetailsView1.Rows[2].Visible = false;
                    DetailsView1.Rows[3].Visible = false;
                    DetailsView1.Rows[4].Visible = false;
                    DetailsView1.Rows[6].Visible = false;

                    DetailsView1.Width = Unit.Pixel(200);
                    label_description_header.Visible = true;
                    label_description.Visible = true;
                    label_description.Text = DetailsView1.Rows[2].Cells[1].Text;
                    if (DetailsView1.CurrentMode == DetailsViewMode.ReadOnly)
                    {

                        DetailsView1.Fields[0].Visible = true;
                        DetailsView1.Fields[1].Visible = true;
                        (DetailsView1.FindControl("LabelEmployeePermition") as Label).Text = DetailsView1.Rows[4].Cells[1].Text;
                        (DetailsView1.FindControl("LabelStatus") as Label).Text = DetailsView1.Rows[6].Cells[1].Text;
                    }
                }

            }
            else
            {
                DetailsView1.SetPageIndex(-1);
                DetailsView1.DataBind();
                label_description_header.Visible = false;
                label_description.Visible = false;
            }
        }
        catch (Exception ex)
        {
            labelError.Visible = true;
            labelError.ForeColor = System.Drawing.Color.Red;
            labelError.Text = ex.Message;
        }


        try  //view the tasks that accepted ,status=finished,in progress,waiting
        {
            if (GridView1.SelectedIndex >= 0)
            {

                var Id = GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text; //saves the id for the table sorting "because details view not supporting sorting"
                if (Session["gridview"] == "GetAcceptedTasks")
                {
                    FillCache();

                    //fill the details view with the selected row data from grid view
                    string exp = string.Format("TaskId={0}", Id);  //the sort expretion 
                    var gridview1_row = (Cache["table"] as DataTable).Select(exp);
                    DetailsView1.DataSource = gridview1_row.CopyToDataTable<DataRow>();
                    DetailsView1.DataBind();

                    DetailsView1.Rows[0].Enabled = false;
                    DetailsView1.Rows[1].Visible = false;
                    DetailsView1.Rows[2].Visible = false;
                    DetailsView1.Rows[3].Visible = false;
                    DetailsView1.Rows[4].Visible = false;
                    DetailsView1.Rows[6].Visible = false;

                    DetailsView1.Width = Unit.Pixel(200);
                    label_description_header.Visible = true;
                    label_description.Visible = true;
                    label_description.Text = DetailsView1.Rows[2].Cells[1].Text;
                    if (DetailsView1.CurrentMode == DetailsViewMode.ReadOnly)
                    {

                        DetailsView1.Fields[0].Visible = true;
                        DetailsView1.Fields[1].Visible = true;
                        (DetailsView1.FindControl("LabelEmployeePermition") as Label).Text = DetailsView1.Rows[4].Cells[1].Text;
                        (DetailsView1.FindControl("LabelStatus") as Label).Text = DetailsView1.Rows[6].Cells[1].Text;
                    }
                }

            }
            else
            {
                DetailsView1.SetPageIndex(-1);
                DetailsView1.DataBind();
                label_description_header.Visible = false;
                label_description.Visible = false;
            }
        }
        catch (Exception ex)
        {
            labelError.Visible = true;
            labelError.ForeColor = System.Drawing.Color.Red;
            labelError.Text = ex.Message;
        }
    }


    //change the mode to edit /update
    protected void DetailsView1_ModeChanging(object sender, DetailsViewModeEventArgs e)
    {
        try
        {
            FillCache();//check if the cache is empty then fill it
            Cache["description"] = label_description.Text;
            DetailsView1.ChangeMode(e.NewMode);
                BindDetailsView1();
                label_description.Text = Cache["description"].ToString();


                if (e.NewMode == DetailsViewMode.Edit)
                {
                    localhost.WebServiceOperatingSystem ws = new localhost.WebServiceOperatingSystem();
                    (DetailsView1.FindControl("DetailsView1DropDownListEmployeePermition") as DropDownList).DataSource
                                 = ws.GetEnum_List(localhost.EnumTaskColumnName.EmployeePermition);
                    (DetailsView1.FindControl("DetailsView1DropDownListEmployeePermition") as DropDownList).DataBind();

                    (DetailsView1.FindControl("DetailsView1DropDownListStatus") as DropDownList).DataSource
                                     = ws.GetEnum_List(localhost.EnumTaskColumnName.Status);
                    (DetailsView1.FindControl("DetailsView1DropDownListStatus") as DropDownList).DataBind();
                }
        }
        catch (Exception ex)
        {
            labelError.Visible = true;
            labelError.ForeColor = System.Drawing.Color.Red;
            labelError.Text = ex.Message;
        }

    }



    protected void DetailsView1_ItemUpdating1(object sender, DetailsViewUpdateEventArgs e)
    {

        //update task
            try
            {
                localhost.WebServiceOperatingSystem ws = new localhost.WebServiceOperatingSystem();

                int TaskId = int.Parse(e.NewValues["TaskId"].ToString());
                string Status = (DetailsView1.FindControl("DetailsView1DropDownListStatus") as DropDownList).SelectedValue;
                string EmployeePermition = (DetailsView1.FindControl("DetailsView1DropDownListEmployeePermition") as DropDownList).SelectedValue;
                string EmployeeDescription = e.NewValues["EmployeeDescription"] == null ? string.Empty : e.NewValues["EmployeeDescription"].ToString();

                int ws_result = ws.UpdateTaskByEmployee(TaskId, EmployeePermition, EmployeeDescription, Status);
                labelError.Text = ws_result == 1 ? "task updated successfully" : ws_result == 111 ? "connection error task not updated " : ws_result == 112 ? ws.Get_sql_exeption() : "unknown error";
                labelError.ForeColor = ws_result == 1 ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                labelError.Visible = true;
                DetailsView1.ChangeMode(DetailsViewMode.ReadOnly);
            }
            catch (Exception ex)
            {
                labelError.Visible = true;
                labelError.ForeColor = System.Drawing.Color.Red;
                labelError.Text = ex.Message;
            }
            //back the details view to read only mood
            finally
            {
               DetailsView1.ChangeMode(DetailsViewMode.ReadOnly);
            if (Session["gridview"] == "GetNewTasks")
                GetNewTasks();
            else
                GetAcceptedTasks();
            }
           
    }


    protected void Button_GetNewTasks_Click(object sender, EventArgs e)
    {
        GetNewTasks();
    }

    protected DataTable GetNewTasks()
    {
        DetailsView1.ChangeMode(DetailsViewMode.ReadOnly);
        int flag = 0; //if tis is the firse binding set the selected index to 0 else leave it to its value
        if (Session["gridview"] == "GetNewTasks") { flag = 1; }
        Session["gridview"] = "GetNewTasks";//saving the method that shown in the grid vied for the updating
        FillCache();
        check_new_task_Click(this, new EventArgs()); //get the number of the new tasks and display it on the new tasks buttton
        DataTable ta = new DataTable();
        try
        {
            ta = (DataTable)Cache["table"];
            if (ta.Rows.Count > 0)
                ta = ta.Select("EmployeePermition='UnKnown'").CopyToDataTable();

            ta.Columns.Remove("TaskDescription");
            ta.Columns.Remove("EmployeePermition");
            ta.Columns.Remove("EmployeeDescription");

            GridView1.DataSource = ta;
            GridView1.DataBind();
            if (GridView1.SelectedIndex > 0 && flag == 0)
            {
                GridView1.SelectedIndex = 0;
                BindDetailsView1();

            }
            else
                BindDetailsView1();
        }
        catch (Exception ex)
        {
            labelError.Visible = true;
            labelError.ForeColor = System.Drawing.Color.Red;
            labelError.Text = ex.Message;
        }

        return ta;

    }

    protected void Button_GetAcceptedTasks_Click(object sender, EventArgs e)
    {
        GetAcceptedTasks();
    }

    protected DataTable GetAcceptedTasks()
    {
        DetailsView1.ChangeMode(DetailsViewMode.ReadOnly);
        int flag = 0;
        if (Session["gridview"] == "GetAcceptedTasks") { flag = 1; }
        Session["gridview"] = "GetAcceptedTasks";//saving the method that shown in the grid vied for the updating
        FillCache();
        DataTable ta = new DataTable();
        try
        {
            ta = (DataTable)Cache["table"];
            ta = ta.Select("EmployeePermition='accepted'").CopyToDataTable();

            ta.Columns.Remove("TaskDescription");
            ta.Columns.Remove("EmployeePermition");
            ta.Columns.Remove("EmployeeDescription");

            GridView1.DataSource = ta;
            GridView1.DataBind();
            if (GridView1.SelectedIndex > 0 && flag == 0)
            {
                GridView1.SelectedIndex = 0;
                BindDetailsView1();

            }
            else
                BindDetailsView1();


        }
        catch (Exception ex)
        {
            labelError.Visible = true;
            labelError.ForeColor = System.Drawing.Color.Red;
            labelError.Text = ex.Message;
        }
        return ta;
    }
    protected void check_new_task_Click(object sender, EventArgs e)
    {
        DataTable ta = new DataTable();
        localhost.WebServiceOperatingSystem ws = new localhost.WebServiceOperatingSystem();
        ta = ws.GetTasksByEmployeeId(Session["UserName"].ToString(), Session["Password"].ToString(), (int)Session["Id"]);
        var count = ta.Select("EmployeePermition='Unknown'").Count();
        new_tasks_number.Text = count.ToString();

    }
}
