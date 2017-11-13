
namespace EnumTasks
{
    using System;


    public enum EnumTaskPriority  //the  priorities of the tasks we need this enumes to insert values by drop down lists
    {
        a = 1, b = 2, c = 3, d = 4, e = 5
    }
    public enum EnumTaskStatus   //status states
    {
        InProgress = 1, Canceled = 2, Finished = 3, Waiting = 4
    }

    public enum EnumTaskEmployeePermition   
    {
        accepted=1,not_accepted,UnKnown
    }

    public enum EnumTaskColumnName //the columns that need specific values   and i need to get there values for the drop down lists to insert values  
    {
        TaskPriority = 1, Status, EmployeePermition
    }
   
}
