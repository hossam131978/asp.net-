using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using EnumTasks;

enum EmployeesTasks_sp  //enum that contains the stored procedure of the OperatingSystem database
{
    sp_GetAllEmployees   //returns a table  for all  manager employees (@ManagerPassword,@ManagerUserName )
        ,
    sp_GetManagers       //returns a table  for all  managers  (@ManagerPassword,@ManagerUserName ) 
        ,
    sp_CheckPassword     //returns (3 if manager and field=head),(2 if manager),(1 if employee),(-1 not exists),  (@UserName nvarchar(50),@Password nvarchar(50),@Name nvarchar(50) output,@Id int output)
        ,
    sp_GetEmployeesByManagerId // get table of the manager employees (@ManagerUserName  nvarchar(50),@ManagerPassword nvarchar(50),@ManagerId int)                                                                                   
        ,
    sp_AddManager        //add a manager (   @ManagerId  int,@ManagerName nvarchar(50), @ManagerField nvarchar(50),@ManagerPassword nvarchar(50),@ManagerUserName nvarchar(50))                                                                                             
        ,
    sp_AddEmployee             //add employee ( @EmployeeId  int, @EmployeeName   nvarchar(50),@EmployeeUserName nvarchar(50) ,@EmployeePassword nvarchar(50),@ManagerId  int )                                                                                             
        ,
    sp_AddTask                 // add a task "returns 1 if added ,0 invalid data,-1 id exists,111 connection error"  (@TaskId int,@ManagerId  int,@EmployeeId  int,@TaskDescription nvarchar(50),@TaskFulfillmentTime int ,@TaskPriority  tinyint)
        ,
    sp_GetManagerEmployeesId    //returns list of 'id' for all  manager employees (@ManagerPassword,@ManagerUserName,@ManagerId)
        ,
    sp_GetTasksIdsAndEmployeesIds //get table contains tasks id's and employees id's  of specific manager (@ManagerId int)
        ,
    sp_MoveTask                   //move task from source employee to destination employee (@TaskId int,@EmployeeId int)
        ,
    sp_UpdateEmployeeByManagerId       //update employee (@nEmployeeId int   --the new employee id ,@oEmployeeId int --the old employee id,@EmployeeName nvarchar(50) ,@EmployeeUserName nvarchar(50),@EmployeePassword nvarchar(50))
        ,
    sp_GetTasksByManagerId      // get table of the manager employees tasks (@ManagerUserName  nvarchar(50),@ManagerPassword nvarchar(50),@ManagerId int)   
        ,
    sp_UpdateTaskByManagerId  // update the manager employees tasks "returns 1 if added ,0 invalid data,-1 id exists,111 connection error"  (@TaskId int,@TaskDescription nvarchar(50),@TaskFulfillmentTime int ,@TaskPriority  tinyint,@Status  nvarchar(50),@EmployeeId  int)
        ,
    sp_GetTasksByEmployeeId   // get table of the manager employees tasks (@EmployeeUserName  nvarchar(50),@EmployeePassword nvarchar(50),@EmployeeId int) 
        ,
    sp_UpdateTaskByEmployee   // // update the employee tasks "returns 1 if updated ,0 invalid data,-1  not updated,111 connection error"  (@TaskId int,@EmployeePermition nvarchar(50),@EmployeeDescription nvarchar(50),@Status  nvarchar(50))
}

/* (copy table from database to another database)  to copy all data table to one of the operating system tables use this function
     public void CopyDataTableToSqlTable()
  {
 //source table
     SqlConnection con = new SqlConnection
            (ConfigurationManager.ConnectionStrings["OperatingSystemConnectionString"].ConnectionString);  //the source table that you want to copy
        SqlCommand com = new SqlCommand("select * from Tasks", con);
        DataTable dt =  new DataTable("dt");
        SqlDataAdapter adap = new SqlDataAdapter(com);
        adap.Fill(dt);
  
 //destination table 
        con.ConnectionString =
        (ConfigurationManager.ConnectionStrings["local_operatingSystem_connectionstring"].ConnectionString); //the destination table you want to fill
        com.Connection = con;
        com.CommandText = "sp_CopyTasks";
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.AddWithValue("@tabletocopy", dt);
        con.Open();
        com.ExecuteNonQuery();
        con.Close();
  }
  
  
  //  the type and the stored procedure syntax
  //   create type TasksTAble 
//   as table
//  (
//    [TaskId] [int] NOT NULL,
//    [ManagerId] [int] NOT NULL,
//    [TaskDescription] [nvarchar](250) NOT NULL,
//    [TaskTypingDate] [smalldatetime] NOT NULL  ,
//    [TaskFulfillmentTime] [int] NOT NULL,
//    [TaskPriority] [int] NOT NULL,
//    [Status] [nvarchar](50) NOT NULL  ,
//    [EmployeeId] [int] NOT NULL,
//    [EmployeePermition] [nvarchar](50) NOT NULL  ,
//    [EmployeeDescription] [nvarchar](150) NOT NULL 
    )
//go
//CREATE PROCEDURE sp_CopyTasks
	 
//    @tabletocopy TasksTAble readonly
//AS
//begin
//    insert into Tasks select * from @tabletocopy
//    end
*/


/// <summary>                                                                           



/// Summary description for WebService  
/// </summary>                          
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
   [System.Web.Script.Services.ScriptService]
public class WebServiceOperatingSystem : System.Web.Services.WebService
{
    public static SqlConnection con;
    public static SqlCommand com;
    public static SqlDataAdapter adap;
    DataSet ds = new DataSet();
    public int return_value = 0;
    DataTable dt =  new DataTable("dt");
    public  string sql_exeption = "";
    public WebServiceOperatingSystem()
    {
        try
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["local_operatingSystem_connectionstring"].ConnectionString);
            com = new SqlCommand("", con);
            adap = new SqlDataAdapter(com);

        }
        catch (Exception ex)
        {
            throw new System.Exception("connection error while trying to connect to webservice", ex);
        }

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    //                         properties 
    [WebMethod]
    public string Get_sql_exeption()
    {
        return this.sql_exeption;
    }
    //[WebMethod]

    //public int Get_Id()
    //{
    //    return Id;
    //}
    //[WebMethod]
    //public string Get_Name()
    //{
    //    return this.Name;
    //}

    //----------------------------------------methods for getting values or names from enum  for drop down lists-----------------------------------------------------------

    [WebMethod]
    public List<int> GetTaskPriorityList()
    {
        List<int> enum_list = new List<int>();
        foreach (var item in Enum.GetValues(typeof(EnumTaskPriority)))
        {
            enum_list.Add((int)item);
        }
        return enum_list;

    }

    [WebMethod]
    public List<string> GetTaskStatusList()
    {
        List<string> enum_list = new List<string>();
        try
        {
            foreach (var item in Enum.GetValues(typeof(EnumTaskStatus)))
            {
                enum_list.Add(item.ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return enum_list;

    }

    [WebMethod]
    public List<string> GetTaskEmployeePermitionList()
    {
        List<string> enum_list = new List<string>();
        try
        {
            foreach (var item in Enum.GetValues(typeof(EnumTaskEmployeePermition)))
            {
                enum_list.Add(item.ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return enum_list;

    }

    [WebMethod]
    public List<object> GetEnum_List(EnumTaskColumnName column)  //gets the enum values or names list  by column name "from data base table columns (EnumTaskColumnName)"
    {

        List<object> enum_list = new List<object>();
        if (column == EnumTaskColumnName.Status)
            foreach (var item in GetTaskStatusList())
            {
                enum_list.Add((object)item);
            }
        if (column == EnumTaskColumnName.TaskPriority)
            foreach (var item in GetTaskPriorityList())
            {
                enum_list.Add((object)item);
            }
        if (column == EnumTaskColumnName.EmployeePermition)
            foreach (var item in GetTaskEmployeePermitionList())
            {
                enum_list.Add((object)item);
            }
        return enum_list;

    }

    //----------------------------------------------------------------------end--------------------------------------------------------------------------------------


    [WebMethod] //get all employees  "need to supply manager user name and password
    public DataTable GetAllEmployees(string ManagerUserName, string ManagerPassword)
    {
        dt.Clear();

        try
        {
            adap.SelectCommand.Parameters.Clear();
            com.Parameters.Clear();
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = EmployeesTasks_sp.sp_GetAllEmployees.ToString();

            com.Parameters.Add(new SqlParameter("@ManagerUserName", SqlDbType.NVarChar, 50)).Value = ManagerUserName;
            com.Parameters.Add(new SqlParameter("@ManagerPassword", SqlDbType.NVarChar, 50)).Value = ManagerPassword;
            com.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

            adap.Fill(dt);
            return_value = (int)adap.SelectCommand.Parameters["@ReturnValue"].Value;
        }
        catch (Exception ex)
        {
            if (ex is SqlException)
            { sql_exeption = ex.Message; }
            dt = null;
        }
        return dt;
    }

    [WebMethod(EnableSession=true)] // check the password if exists and returns an integer  
    public string CheckPassword(string UserName, string Password)
    {
        string ret="";
          string  return_value = "0";
        try
        {
            com.Parameters.Clear();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = EmployeesTasks_sp.sp_CheckPassword.ToString();
            com.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 50)).Value = UserName;
            com.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 50)).Value = Password;
            com.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            com.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            com.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            return_value =adap.SelectCommand.Parameters["@ReturnValue"].Value.ToString();
            if (return_value.Equals("1") || return_value.Equals("3") || return_value.Equals("2"))
            {
                 ret =  (int)com.Parameters["@Id"].Value + "," + (string)com.Parameters["@Name"].Value;
            }
            else
                ret = 0 + "," + "none";
        }
        catch (Exception ex)
        {
            if (ex is SqlException)
            { sql_exeption = ex.Message; return_value= "112"; }
            return_value ="111";
        }
        //if the "return_value"= "0"    then there is a problem with the connection 
        //if the "return_value"= "3"    then the user name is the head master 
        //if the "return_value"= "2"    then the user name is a manager
        //if the "return_value"= "1"    then the user name is a employee
        //if the "return_value"= "-1"   then the user name is not exist's
        //if the "return_value"= "111"   connection error 
        //if the "return_value"= "112"   error in database


        return ret+","+return_value;
    }

    [WebMethod] //get all the managers data
    public DataTable GetManagers(string ManagerUserName, string ManagerPassword)
    {
       dt.Clear();
        try
        {
            adap.SelectCommand.Parameters.Clear();
            com.Parameters.Clear();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = EmployeesTasks_sp.sp_GetManagers.ToString();

            com.Parameters.Add(new SqlParameter("@ManagerUserName", SqlDbType.NVarChar, 50)).Value = ManagerUserName;
            com.Parameters.Add(new SqlParameter("@ManagerPassword", SqlDbType.NVarChar, 50)).Value = ManagerPassword;
            com.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

            adap.Fill(dt);
            return_value = (int)adap.SelectCommand.Parameters["@ReturnValue"].Value;
        }
        catch (Exception ex)
        {
            if (ex is SqlException)
            { sql_exeption = ex.Message; }
            dt = null;
        }
        return dt;
    }

    [WebMethod]  //get the tasks of the manager  
    public DataTable GetTasksByManagerId(string ManagerUserName, string ManagerPassword, int ManagerId)
    {
        dt.Clear();

        try
        {
            adap.SelectCommand.Parameters.Clear();
            com.Parameters.Clear();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = EmployeesTasks_sp.sp_GetTasksByManagerId.ToString();

            com.Parameters.Add(new SqlParameter("@ManagerUserName", SqlDbType.NVarChar, 50)).Value = ManagerUserName;
            com.Parameters.Add(new SqlParameter("@ManagerPassword", SqlDbType.NVarChar, 50)).Value = ManagerPassword;
            com.Parameters.Add(new SqlParameter("@ManagerId", SqlDbType.Int)).Value = ManagerId;

            com.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

            adap.Fill(dt);
            return_value = (int)adap.SelectCommand.Parameters["@ReturnValue"].Value;
        }
        catch (Exception ex)
        {
            if (ex is SqlException)
            { sql_exeption = ex.Message; }
            dt = null;
        }
        return dt;
    }

    [WebMethod]  //get the tasks of the employee
    public DataTable GetTasksByEmployeeId(string EmployeeUserName, string EmployeePassword, int EmployeeId)
    {
        dt.Clear();

        try
        {
            adap.SelectCommand.Parameters.Clear();
            com.Parameters.Clear();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = EmployeesTasks_sp.sp_GetTasksByEmployeeId.ToString();

            com.Parameters.Add(new SqlParameter("@EmployeeUserName", SqlDbType.NVarChar, 50)).Value = EmployeeUserName;
            com.Parameters.Add(new SqlParameter("@EmployeePassword", SqlDbType.NVarChar, 50)).Value = EmployeePassword;
            com.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int)).Value = EmployeeId;

            com.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

            adap.Fill(dt);
            return_value = (int)adap.SelectCommand.Parameters["@ReturnValue"].Value;
        }
        catch (Exception ex)
        {
            if (ex is SqlException)
            { sql_exeption = ex.Message; }
            dt = null;
        }
        return dt;
    }

    [WebMethod] //get the manager employees
    public DataTable GetEmployeesByManagerId(string ManagerUserName, string ManagerPassword, int ManagerId)
    {
        dt.Clear();

        try
        {
            adap.SelectCommand.Parameters.Clear();
            com.Parameters.Clear();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = EmployeesTasks_sp.sp_GetEmployeesByManagerId.ToString();

            com.Parameters.Add(new SqlParameter("@ManagerUserName", SqlDbType.NVarChar, 50)).Value = ManagerUserName;
            com.Parameters.Add(new SqlParameter("@ManagerPassword", SqlDbType.NVarChar, 50)).Value = ManagerPassword;
            com.Parameters.Add(new SqlParameter("@ManagerId", SqlDbType.Int)).Value = ManagerId;

            com.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

            adap.Fill(dt);
            return_value = (int)adap.SelectCommand.Parameters["@ReturnValue"].Value;
        }
        catch (Exception ex)
        {
            if (ex is SqlException)
            { sql_exeption = ex.Message; }
            dt = null;
        }
        return dt;
    }

    [WebMethod] //add manager to the managers table
    public int AddManager(string ManagerUserName, string ManagerPassword, int ManagerId, string ManagerName, string ManagerField)
    {
        try
        {
            //  adap.SelectCommand.Parameters.Clear();
            com.Parameters.Clear();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = EmployeesTasks_sp.sp_AddManager.ToString();
            com.Parameters.Add(new SqlParameter("@ManagerId", SqlDbType.Int)).Value = ManagerId;
            com.Parameters.Add(new SqlParameter("@ManagerName", SqlDbType.NVarChar, 50)).Value = ManagerName;
            com.Parameters.Add(new SqlParameter("@ManagerField", SqlDbType.NVarChar, 50)).Value = ManagerField;
            com.Parameters.Add(new SqlParameter("@ManagerPassword", SqlDbType.NVarChar, 50)).Value = ManagerPassword;
            com.Parameters.Add(new SqlParameter("@ManagerUserName", SqlDbType.NVarChar, 50)).Value = ManagerUserName;

            com.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            return_value = (int)com.Parameters["@ReturnValue"].Value;
        }

        catch (Exception ex)
        {
            if (ex is SqlException)
            { sql_exeption = ex.Message; return 112; }
            return 111;
        }
        return return_value;
    }

    [WebMethod] //add employee to the employees table
    public int AddEmployee(int EmployeeId, string EmployeeName, string EmployeeUserName, string EmployeePassword, int ManagerId)
    {
        try
        {
            //  adap.SelectCommand.Parameters.Clear();
            com.Parameters.Clear();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = EmployeesTasks_sp.sp_AddEmployee.ToString();
            com.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int)).Value = EmployeeId;
            com.Parameters.Add(new SqlParameter("@EmployeeName", SqlDbType.NVarChar, 50)).Value = EmployeeName;
            com.Parameters.Add(new SqlParameter("@EmployeeUserName", SqlDbType.NVarChar, 50)).Value = EmployeeUserName;
            com.Parameters.Add(new SqlParameter("EmployeePassword", SqlDbType.NVarChar, 50)).Value = EmployeePassword;
            com.Parameters.Add(new SqlParameter("@ManagerId", SqlDbType.Int)).Value = ManagerId;


            com.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            //adap.InsertCommand.CommandType = CommandType.StoredProcedure;
            //adap.InsertCommand = com;
            //adap.InsertCommand.ExecuteNonQuery();
            //adap.SelectCommand.ExecuteNonQuery();
            return_value = (int)com.Parameters["@ReturnValue"].Value;
        }
        catch (Exception ex)
        {
            if (ex is SqlException)
            { sql_exeption = ex.Message; return 112; }
            return return_value = 111;
        }
        return return_value;
    }

    [WebMethod]
    public int AddTask(int TaskId, int ManagerId, int EmployeeId, string TaskDescription, int TaskFulfillmentTime, int TaskPriority)
    {
        try
        {
            com.Parameters.Clear();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = EmployeesTasks_sp.sp_AddTask.ToString();
            com.Parameters.Add(new SqlParameter("@TaskId", SqlDbType.Int)).Value = TaskId;
            com.Parameters.Add(new SqlParameter("@ManagerId", SqlDbType.Int)).Value = ManagerId;
            com.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int)).Value = EmployeeId;
            com.Parameters.Add(new SqlParameter("@TaskDescription", SqlDbType.NVarChar, 50)).Value = TaskDescription;
            com.Parameters.Add(new SqlParameter("@TaskFulfillmentTime", SqlDbType.Int)).Value = TaskFulfillmentTime;
            com.Parameters.Add(new SqlParameter("@TaskPriority", SqlDbType.Int)).Value = TaskPriority;

            com.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            return_value = (int)com.Parameters["@ReturnValue"].Value;
        }
        catch (Exception ex)
        {
            if (ex is SqlException)
            { sql_exeption = ex.Message; return 112; }
            return return_value = 111;
        }
        return return_value;
    }

    [WebMethod] //get the id's of the manager employees
    public DataTable GetManagerEmployeesId(string ManagerUserName, string ManagerPassword, int ManagerId)
    {
        dt.Reset();
        try
        {
            adap.SelectCommand.Parameters.Clear();
            com.Parameters.Clear();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = EmployeesTasks_sp.sp_GetManagerEmployeesId.ToString();

            com.Parameters.Add(new SqlParameter("@ManagerPassword", SqlDbType.NVarChar, 50)).Value = ManagerPassword;
            com.Parameters.Add(new SqlParameter("@ManagerId", SqlDbType.Int)).Value = ManagerId;
            com.Parameters.Add(new SqlParameter("@ManagerUserName", SqlDbType.NVarChar, 50)).Value = ManagerUserName;

            com.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

            adap.Fill(dt);
            return_value = (int)adap.SelectCommand.Parameters["@ReturnValue"].Value;
        }
        catch (Exception ex)
        {
            if (ex is SqlException)
            { sql_exeption = ex.Message; }
            dt = null;
        }
        return dt;


    }

    [WebMethod]
    public DataTable GetTasksIdsAndEmployeesIds(int ManagerId)
    {
        dt.Clear();

        try
        {
            adap.SelectCommand.Parameters.Clear();
            com.Parameters.Clear();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = EmployeesTasks_sp.sp_GetTasksIdsAndEmployeesIds.ToString();
            com.Parameters.Add(new SqlParameter("@ManagerId", SqlDbType.Int)).Value = ManagerId;
            com.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            adap.Fill(dt);
            return_value = (int)adap.SelectCommand.Parameters["@ReturnValue"].Value;
        }
        catch (Exception ex)
        {
            if (ex is SqlException)
            { sql_exeption = ex.Message; }
            dt = null;
        }


        return dt;


    }


    [WebMethod] //move task from employee to another 
    public int MoveTask(int TaskId, int DestinationEmployeeId)
    {
        try
        {
            com.Parameters.Clear();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = EmployeesTasks_sp.sp_MoveTask.ToString();
            com.Parameters.Add(new SqlParameter("@TaskId", SqlDbType.Int)).Value = TaskId;
            com.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int)).Value = DestinationEmployeeId;

            com.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            return_value = (int)com.Parameters["@ReturnValue"].Value;
        }
        catch (Exception ex)
        {
            if (ex is SqlException)
            { sql_exeption = ex.Message; return 112; }
            return 111;
        }
        return return_value;
    }

    [WebMethod]
    public int UpdateEmployeeByManagerId(int nEmployeeId, int oEmployeeId, string EmployeeName, string EmployeeUserName, string EmployeePassword)
    {
        try
        {
            //  adap.SelectCommand.Parameters.Clear();
            com.Parameters.Clear();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = EmployeesTasks_sp.sp_UpdateEmployeeByManagerId.ToString();
            com.Parameters.Add(new SqlParameter("@nEmployeeId", SqlDbType.Int)).Value = nEmployeeId;
            com.Parameters.Add(new SqlParameter("@oEmployeeId", SqlDbType.Int)).Value = oEmployeeId;
            com.Parameters.Add(new SqlParameter("@EmployeeName", SqlDbType.NVarChar, 50)).Value = EmployeeName;
            com.Parameters.Add(new SqlParameter("@EmployeeUserName", SqlDbType.NVarChar, 50)).Value = EmployeeUserName;
            com.Parameters.Add(new SqlParameter("@EmployeePassword", SqlDbType.NVarChar, 50)).Value = EmployeePassword;

            com.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            return_value = (int)com.Parameters["@ReturnValue"].Value;
        }
        catch (Exception ex)
        {
            if (ex is SqlException)
            { sql_exeption = ex.Message; return 112; }
            return 111;
        }
        return return_value;
    }


    [WebMethod]
    public int UpdateTaskByManagerId(int TaskId, string TaskDescription, int TaskFulfillmentTime, int TaskPriority, string Status, int EmployeeId)
    {


        try
        {
            com.Parameters.Clear();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = EmployeesTasks_sp.sp_UpdateTaskByManagerId.ToString();
            com.Parameters.Add(new SqlParameter("@TaskId", SqlDbType.Int)).Value = TaskId;
            com.Parameters.Add(new SqlParameter("@TaskDescription", SqlDbType.NVarChar, 50)).Value = TaskDescription;
            com.Parameters.Add(new SqlParameter("@TaskFulfillmentTime", SqlDbType.Int)).Value = TaskFulfillmentTime;
            com.Parameters.Add(new SqlParameter("@TaskPriority", SqlDbType.Int)).Value = TaskPriority;
            com.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar, 50)).Value = Status == null ? "" : Status;
            com.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int)).Value = EmployeeId;

            com.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            return_value = (int)com.Parameters["@ReturnValue"].Value;
        }
        catch (Exception ex)
        {
            if (ex is SqlException)
            { sql_exeption = ex.Message; return 112; }
            return 111;
        }
        return return_value;
    }


    [WebMethod]
    public int UpdateTaskByEmployee(int TaskId, string EmployeePermition, string EmployeeDescription, string Status)
    {


        try
        {
            com.Parameters.Clear();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = EmployeesTasks_sp.sp_UpdateTaskByEmployee.ToString();
            com.Parameters.Add(new SqlParameter("@TaskId", SqlDbType.Int)).Value = TaskId;
            com.Parameters.Add(new SqlParameter("@EmployeePermition", SqlDbType.NVarChar, 50)).Value = EmployeePermition;
            com.Parameters.Add(new SqlParameter("@EmployeeDescription", SqlDbType.NVarChar, 50)).Value = EmployeeDescription;
            com.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar, 50)).Value = Status == null ? "" : Status;

            com.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            return_value = (int)com.Parameters["@ReturnValue"].Value;
        }
        catch (Exception ex)
        {
            if (ex is SqlException)
            { sql_exeption = ex.Message; return 112; }
            return 111;
        }
        return return_value;
    }

}