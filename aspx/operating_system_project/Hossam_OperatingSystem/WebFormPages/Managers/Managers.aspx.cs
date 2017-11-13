using EnumTasks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Managers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        labelError.Visible = false; //the label that shows the exceptions or updating state

        if (Session["gridview"] != "GetTaskBy")
        {
            DropDownListFilter.Visible = false;
            DropDownListGetTaskBy.Visible = false;
        }
        if (Session["UserName"] != null && Session["Password"] != null)
        {
            if (!IsPostBack)
            {
                DropDownListFilter.Visible = false;
                DropDownListGetTaskBy.Visible = false;
                GridView1.SelectedIndex = 0;
                GetEmployees();
                UserName_Label.Text = "Welcome  <b> " + Session["Name"].ToString() + "</b>";
            }
        }
        else Response.Redirect(@"~/Login.aspx");
    }

    
    protected void Button_AddEmployee_Click(object sender, EventArgs e)
    {

        if (Session["UserName"] != null && Session["Password"] != null)
        {
            Response.Redirect("~/WebFormPages/Managers/AddEmployees.aspx");
        }
        else Response.Redirect(@"~/Login.aspx");
    }
    protected void Button_GetEmployees_Click(object sender, EventArgs e)
    {
        GetEmployees();

    }
    protected void GetEmployees()
    {

        int flag = 0;
        if (Session["gridview"] == "GetEmployees") { flag = 1; }
        Session["gridview"] = "GetEmployees";//saving the method that shown in the grid vied for the updating and other operatings
        FillCache();

        try
        {
            GridView1.DataSource = Cache["table"];
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



    }

    protected void Button_AddTasks_Click(object sender, EventArgs e)
    {
        if (Session["UserName"] != null && Session["Password"] != null)
        {
            Response.Redirect("~/WebFormPages/Managers/AddTasks.aspx");
        }
        else Response.Redirect(@"~/Login.aspx");
    }
    protected void Button_moveTask_Click(object sender, EventArgs e)
    {
        if (Session["UserName"] != null && Session["Password"] != null)
        {
            Response.Redirect("~/WebFormPages/Managers/MoveTasks.aspx");
        }
        else Response.Redirect(@"~/Login.aspx");
    }
    protected void Button_GetTask_Click(object sender, EventArgs e)
    {
        GetTasks();

    }
    protected void GetTasks()
    {
        int flag = 0;
        if (Session["gridview"] == "GetTasks") { flag = 1; }
        Session["gridview"] = "GetTasks";//saving the method that shown in the grid vied for the updating
        FillCache();
        try
        {
            GridView1.DataSource = Cache["table"];
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


    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {

        
        try
        {
            FillCache();//check if the cache is empty then fill it
            DataTable ta = new DataTable();
            ta = (DataTable)Cache["table"];

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
    private string GetSortDirection(string column)
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

    protected void GridView1_SelectedIndexChanging(object sender, EventArgs e)
    {

        FillCache();//check if the cache is empty then fill it
        DetailsView1.ChangeMode(DetailsViewMode.ReadOnly); //returns the mode to read only if it was updating?
        BindDetailsView1();

    }

    private void BindDetailsView1() 
    {
        try
        {


            if (GridView1.SelectedIndex >= 0)
            {
                FillCache();
                var Id = GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text;
                //hide the none relevant fields
                if (Session["gridview"] == "GetEmployees")
                {
                    //gets the selected row in the grid view by task id
                    if (DetailsView1.Fields.Count > 0)
                    {
                        DetailsView1.Fields[0].Visible = false;
                        DetailsView1.Fields[1].Visible = false;
                    }
                    string exp = string.Format("EmployeeId={0}", Id);  //the sort expretion 
                    var gridview1_row = (Cache["table"] as DataTable).Select(exp);
                    DetailsView1.DataSource = gridview1_row.CopyToDataTable<DataRow>();

                    DetailsView1.DataBind();
                    DetailsView1.Rows[4].Visible = false;
                    DetailsView1.Rows[5].Visible = false;

                    

                }
                else

                    if (Session["gridview"] == "GetTasks" || Session["gridview"] == "GetTaskBy")
                    {


                        //fill the details view with the selected row data from grid view
                        string exp = string.Format("TaskId={0}", Id);  //the sort expretion 
                        var gridview1_row = (Cache["table"] as DataTable).Select(exp);
                        DetailsView1.DataSource = gridview1_row.CopyToDataTable<DataRow>();
                        DetailsView1.DataBind();
                        DetailsView1.Rows[0].Enabled = false;
                        DetailsView1.Rows[1].Visible = false;
                        DetailsView1.Rows[3].Visible = false;
                        DetailsView1.Rows[5].Visible = false;
                        DetailsView1.Rows[6].Visible = false;
                        DetailsView1.Rows[7].Visible = false;
                        DetailsView1.Rows[8].Visible = false;
                        DetailsView1.Rows[9].Visible = false;

                        if (DetailsView1.Fields.Count > 0 && DetailsView1.CurrentMode == DetailsViewMode.ReadOnly)
                        {

                            DetailsView1.Fields[0].Visible = true;
                            DetailsView1.Fields[1].Visible = true;
                            (DetailsView1.FindControl("LabelTaskPriority") as Label).Text = DetailsView1.Rows[5].Cells[1].Text;
                            (DetailsView1.FindControl("LabelStatus") as Label).Text = DetailsView1.Rows[6].Cells[1].Text;
                        }
                       
                    }

            }
            else
            {
                DetailsView1.SetPageIndex(-1);
                DetailsView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            labelError.Visible = true;
            labelError.ForeColor = System.Drawing.Color.Red;
            labelError.Text = ex.Message;
        }

        DetailsView1.RowStyle.CssClass = "rows";
    }

    // fill the cache["table"] with the relevant date table
    protected void FillCache()
    {
        //check if there is data saved in the cache 
        if (Session["gridview"] != "GetTaskBy")
            if (Session["gridview"] == "GetTasks") //if the last data of the grid view was tasks
            //if (Cache["table"] == null)
            {
                try
                {
                    localhost.WebServiceOperatingSystem ws = new localhost.WebServiceOperatingSystem();
                    Cache["table"] = ws.GetTasksByManagerId(Session["UserName"].ToString(), Session["Password"].ToString(), (int)Session["Id"]);
                }
                catch (Exception ex)
                {
                    labelError.Visible = true;
                    labelError.ForeColor = System.Drawing.Color.Red;
                    labelError.Text = ex.Message;
                }
            }
            else
                if (Session["gridview"] == "GetEmployees") //if the last data of the grid view was employees
               
                {
                    try
                    {
                        localhost.WebServiceOperatingSystem ws = new localhost.WebServiceOperatingSystem();
                        Cache["table"] = ws.GetEmployeesByManagerId(Session["UserName"].ToString(), Session["Password"].ToString(), (int)Session["Id"]);
                    }
                    catch (Exception ex)
                    {
                        labelError.Visible = true;
                        labelError.ForeColor = System.Drawing.Color.Red;
                        labelError.Text = ex.Message;
                    }

                }

    }

    protected void DetailsView1_ModeChanging(object sender, DetailsViewModeEventArgs e)
    {

        DetailsView1.ChangeMode(e.NewMode);
        BindDetailsView1();
        try
        {

            if (Session["gridview"] == "GetTasks" || Session["gridview"] == "GetTaskBy")
            {
                if (DetailsView1.Fields.Count > 0 && e.NewMode == DetailsViewMode.Edit)
                {
                    //changing the task priority field to drop down list

                    localhost.WebServiceOperatingSystem ws = new localhost.WebServiceOperatingSystem();
                    DetailsView1.Fields[0].Visible = true;
                    DetailsView1.Fields[1].Visible = true;
                    (DetailsView1.FindControl("DetailsView1DropDownListTaskPriority") as DropDownList).DataSource = ws.GetEnum_List(localhost.EnumTaskColumnName.TaskPriority);
                    (DetailsView1.FindControl("DetailsView1DropDownListTaskPriority") as DropDownList).DataBind();

                    (DetailsView1.FindControl("DetailsView1DropDownListStatus") as DropDownList).DataSource = ws.GetEnum_List(localhost.EnumTaskColumnName.Status);
                    (DetailsView1.FindControl("DetailsView1DropDownListStatus") as DropDownList).DataBind();


                }

              
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
        localhost.WebServiceOperatingSystem ws = new localhost.WebServiceOperatingSystem();


        //update employee
        if (Session["gridview"] == "GetEmployees")
            try
            {

                int oEmployeeId = int.Parse(GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text.ToString());
                int nEmployeeId = int.Parse(e.NewValues["EmployeeId"].ToString());
                string EmployeeName = e.NewValues["EmployeeName"].ToString();
                string EmployeeUserName = e.NewValues["EmployeeUserName"].ToString();
                string EmployeePassword = e.NewValues["EmployeePassword"].ToString();
                int ws_result=ws.UpdateEmployeeByManagerId(nEmployeeId, oEmployeeId, EmployeeName, EmployeeUserName, EmployeePassword) ;
                labelError.Text = ws_result == 1 ? "employee updated successfully" : ws_result == 111 ? "connection error  employee not updated " : ws_result == 112 ? (ws.Get_sql_exeption()??"sql exeption") : "unknown error";
                labelError.Visible = true;
                labelError.ForeColor = ws_result == 1 ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                GetEmployees();

            }
            catch (Exception ex)
            {
                labelError.Visible = true;
                labelError.ForeColor = System.Drawing.Color.Red;
                labelError.Text = ex.Message;
            }
            finally
            {
                DetailsView1.ChangeMode(DetailsViewMode.ReadOnly);
                BindDetailsView1();


            }


        //update task
        if (Session["gridview"] == "GetTasks" || Session["gridview"] == "GetTaskBy")
            try
            {

                int TaskId = int.Parse(e.NewValues["TaskId"].ToString());
                string TaskDescription = e.NewValues["TaskDescription"].ToString();
                int TaskFulfillmentTime = int.Parse(e.NewValues["TaskFulfillmentTime"].ToString());
                int TaskPriority = int.Parse((DetailsView1.FindControl("DetailsView1DropDownListTaskPriority") as DropDownList).SelectedValue);
                string Status = (DetailsView1.FindControl("DetailsView1DropDownListStatus") as DropDownList).SelectedValue;
                int EmployeeId = int.Parse(e.NewValues["EmployeeId"].ToString());

                int ws_result = ws.UpdateTaskByManagerId(TaskId, TaskDescription, TaskFulfillmentTime, TaskPriority, Status, EmployeeId) ;
                labelError.Text = ws_result == 1 ? "task updated successfully" : ws_result == 111 ? "connection error  task not updated " : ws_result == 112 ? (ws.Get_sql_exeption()??"sql exeption"): "unknown error";
                labelError.Visible = true;
                labelError.ForeColor = ws_result == 1 ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                labelError.Visible = true;
                DetailsView1.ChangeMode(DetailsViewMode.ReadOnly);
                GetTasks();
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
                BindDetailsView1();
            }
    }
  
    protected void Button_GetTaskBy_Click(object sender, EventArgs e)
    {

        DropDownListFilter.Visible = true;
        DropDownListGetTaskBy.Visible = true;

        Session["gridview"] = "GetTasks";//  the grid view will take the Tasks table data
        FillCache();
        
        DataTable ta = Cache["table"] as DataTable;  
        var list = ta.Columns.Cast<DataColumn>().ToList();

        DropDownListGetTaskBy.DataSource = list;
        DropDownListGetTaskBy.DataBind();

        string column = DropDownListGetTaskBy.SelectedValue;

        var results = (from p in ta.AsEnumerable()
                       select p[column]).Distinct().ToList();

        DropDownListFilter.DataSource = results;
        DropDownListFilter.DataBind();
        DropDownListFilter_SelectedIndexChanged(this, new EventArgs()); //fill the grid view with the filtered table 
    }

    protected void DropDownListGetTaskBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["gridview"] = "GetTasks";//causes the cache to load the tasks table
        FillCache();

        DataTable ta = Cache["table"] as DataTable;

        string column = DropDownListGetTaskBy.SelectedValue;

        var results = (from p in ta.AsEnumerable()
                       select  p[column].ToString()).Distinct().ToList();
        if (column == "TaskTypingDate")
            results=results.Select(x=>x.Substring(0,10)).Distinct().ToList();

        DropDownListFilter.DataSource = results;
        DropDownListFilter.DataBind();
        DropDownListFilter_SelectedIndexChanged(this,new EventArgs());

    }
    
    protected void DropDownListFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {


            Session["gridview"] = "GetTasks";//causes the cache to load the tasks table
            FillCache();
            Session["gridview"] = "GetTaskBy";//causes the cache to load the tasks table

            DataTable ta = Cache["table"] as DataTable;
            string column = DropDownListGetTaskBy.SelectedValue; //column name  
            string condition = DropDownListFilter.SelectedValue;  // column value to search for 
            string expretion = column + "='" + condition + "'";  //search for all the values that match the selected value in the drop down list

            if (column.Equals("TaskTypingDate"))  //special deal for the date of the tasks
            {
                //gets only the date like a string  without the time
                condition = (DateTime.Parse(condition).Date).ToString().Substring(0, 10);

                //searching for the date "not including the time" will return the tasks that created in the same day
                expretion = "CONVERT(TaskTypingDate,System.String) like '%" + condition + "%'";
            }

            DataView dv = new DataView(ta, expretion, column, DataViewRowState.CurrentRows);

            Cache["table"] = dv.ToTable(); //the details view will be related to the sorted table
            GridView1.SelectedIndex = 0;
            GridView1.DataSource = dv;
            GridView1.DataBind();
            BindDetailsView1();

        }
        catch (Exception ex)
        {
            labelError.Visible = true;
            labelError.ForeColor = System.Drawing.Color.Red;
            labelError.Text = ex.Message;
        }
    }
}


