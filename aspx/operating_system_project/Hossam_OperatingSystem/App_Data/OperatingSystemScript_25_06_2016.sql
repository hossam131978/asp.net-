USE [master]
GO
/****** Object:  Database [OperatingSystem]    Script Date: 25/06/2016 16:29:33 ******/
CREATE DATABASE [OperatingSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OperatingSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\OperatingSystem.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'OperatingSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\OperatingSystem.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [OperatingSystem] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OperatingSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OperatingSystem] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [OperatingSystem] SET ANSI_NULLS ON 
GO
ALTER DATABASE [OperatingSystem] SET ANSI_PADDING ON 
GO
ALTER DATABASE [OperatingSystem] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [OperatingSystem] SET ARITHABORT ON 
GO
ALTER DATABASE [OperatingSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OperatingSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OperatingSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OperatingSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OperatingSystem] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [OperatingSystem] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [OperatingSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OperatingSystem] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [OperatingSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OperatingSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OperatingSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OperatingSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OperatingSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OperatingSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OperatingSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OperatingSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OperatingSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OperatingSystem] SET RECOVERY FULL 
GO
ALTER DATABASE [OperatingSystem] SET  MULTI_USER 
GO
ALTER DATABASE [OperatingSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OperatingSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OperatingSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OperatingSystem] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [OperatingSystem] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'OperatingSystem', N'ON'
GO
USE [OperatingSystem]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 25/06/2016 16:29:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeId] [int] NOT NULL,
	[EmployeeName] [nvarchar](50) NOT NULL,
	[EmployeeUserName] [nvarchar](50) NOT NULL,
	[EmployeePassword] [nvarchar](50) NOT NULL,
	[ManagerId] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL CONSTRAINT [DF_Employees_StartDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [unique_EmployeeUserName] UNIQUE NONCLUSTERED 
(
	[EmployeeUserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Managers]    Script Date: 25/06/2016 16:29:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Managers](
	[ManagerId] [int] NOT NULL,
	[ManagerName] [nvarchar](50) NOT NULL,
	[ManagerField] [nvarchar](50) NOT NULL,
	[ManagerPassword] [nvarchar](50) NOT NULL,
	[StartDate] [nvarchar](50) NOT NULL CONSTRAINT [DF_Managers_StartDate]  DEFAULT (getdate()),
	[ManagerUserName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Managers] PRIMARY KEY CLUSTERED 
(
	[ManagerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UserName_Managers] UNIQUE NONCLUSTERED 
(
	[ManagerUserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 25/06/2016 16:29:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[TaskId] [int] NOT NULL,
	[ManagerId] [int] NOT NULL,
	[TaskDescription] [nvarchar](250) NOT NULL,
	[TaskTypingDate] [smalldatetime] NOT NULL CONSTRAINT [DF_Tasks_TaskTypingDate]  DEFAULT (getdate()),
	[TaskFulfillmentTime] [int] NOT NULL,
	[TaskPriority] [int] NOT NULL,
	[Status] [nvarchar](50) NOT NULL CONSTRAINT [DF_Tasks_Status]  DEFAULT ('None'),
	[EmployeeId] [int] NOT NULL,
	[EmployeePermition] [nvarchar](50) NOT NULL CONSTRAINT [DF_Tasks_EmployeePermition]  DEFAULT ('UnKnown'),
	[EmployeeDescription] [nvarchar](150) NOT NULL CONSTRAINT [DF_Tasks_EmployeeDescription]  DEFAULT ('None'),
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TasksPermitions]    Script Date: 25/06/2016 16:29:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TasksPermitions](
	[TaskId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[EmployeePermition] [nvarchar](50) NOT NULL,
	[EmployeeDescription] [nvarchar](150) NOT NULL,
	[DateAndTime] [nvarchar](50) NOT NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TasksPermitions] ADD  CONSTRAINT [DF_TasksPermitions_DateAndTime]  DEFAULT (getdate()) FOR [DateAndTime]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Employees]
GO
ALTER TABLE [dbo].[TasksPermitions]  WITH CHECK ADD  CONSTRAINT [FK_TasksPermitions_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[TasksPermitions] CHECK CONSTRAINT [FK_TasksPermitions_Employees]
GO
ALTER TABLE [dbo].[TasksPermitions]  WITH CHECK ADD  CONSTRAINT [FK_TasksPermitions_Tasks] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Tasks] ([TaskId])
GO
ALTER TABLE [dbo].[TasksPermitions] CHECK CONSTRAINT [FK_TasksPermitions_Tasks]
GO
/****** Object:  StoredProcedure [dbo].[sp_AddEmployee]    Script Date: 25/06/2016 16:29:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_AddEmployee] 
	-- Add the parameters for the stored procedure here
	       @EmployeeId  int,
           @EmployeeName   nvarchar(50),
		   @EmployeeUserName nvarchar(50) ,
	       @EmployeePassword nvarchar(50), 
           @ManagerId  int

AS
BEGIN
if (@EmployeeId>0 
    and @EmployeeName IS NOT NULL And @EmployeeName != ''  
    and @EmployeeUserName IS NOT NULL And @EmployeeUserName != ''  
    and @EmployeePassword IS NOT NULL And @EmployeePassword != ''  
    and @ManagerId IS NOT NULL And @ManagerId != ''  
	)
    

	begin

if  not exists (select * from  [dbo].[Employees] where @EmployeeId=EmployeeId ) 
begin
	
	INSERT INTO  [dbo].[Employees]
           (EmployeeId
           ,EmployeeName 
		   ,EmployeeUserName  
           ,EmployeePassword
                 
           ,ManagerId)
     VALUES
          (@EmployeeId ,
           @EmployeeName ,
           @EmployeeUserName ,  
		   @EmployeePassword ,  
           @ManagerId )
	return 1;
	end
	else
	begin
	return -1; -- the id is exists
	end

 end
  else begin return 0; end  --the data is incorrect
 End



GO
/****** Object:  StoredProcedure [dbo].[sp_AddManager]    Script Date: 25/06/2016 16:29:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_AddManager] 
	-- Add the parameters for the stored procedure here
	       @ManagerId  int,
           @ManagerName nvarchar(50),
           @ManagerField nvarchar(50),
           @ManagerPassword nvarchar(50),         
           @ManagerUserName nvarchar(50) 
AS
BEGIN
if (@ManagerId>0 
    and @ManagerName IS NOT NULL And @ManagerName != ''  
    and @ManagerField IS NOT NULL And @ManagerField != ''  
    and @ManagerPassword IS NOT NULL And @ManagerPassword != ''  
    and @ManagerUserName IS NOT NULL And @ManagerUserName != ''  
	)
    

	begin

if  not exists (select * from Managers where @ManagerId=ManagerId ) 
begin
	
	INSERT INTO [dbo].[Managers]
           ([ManagerId]
           ,[ManagerName]
           ,[ManagerField]
           ,[ManagerPassword]         
           ,[ManagerUserName])
     VALUES
          (@ManagerId ,
           @ManagerName ,
           @ManagerField ,
           @ManagerPassword ,    
           @ManagerUserName )
	return 1;
	end
	else
	begin
	return -1;
	end

 end
  else begin return 0; end
 End







GO
/****** Object:  StoredProcedure [dbo].[sp_AddTask]    Script Date: 25/06/2016 16:29:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_AddTask] 
	-- Add the parameters for the stored procedure here
	        
		   @TaskId int,
		   @ManagerId  int,
		   @EmployeeId  int,
           @TaskDescription nvarchar(50),
		   @TaskFulfillmentTime int ,
	       @TaskPriority  tinyint
	
         

AS
BEGIN
if (@TaskId>0 and @ManagerId>0 and @EmployeeId>0 and
    @TaskFulfillmentTime>0 and @TaskPriority>0 and 
	@TaskDescription IS NOT NULL And @TaskDescription != ''  
	)
	
	BEGIN

if  not exists (select * from [dbo].[Tasks]  where @TaskId=TaskId ) 
BEGIN
	
	INSERT INTO   [dbo].[Tasks]
          (TaskId ,
           ManagerId ,
           EmployeeId ,  
		   TaskFulfillmentTime ,  
           TaskPriority,
		   TaskDescription )
     VALUES
          (@TaskId ,
           @ManagerId ,
           @EmployeeId ,  
		   @TaskFulfillmentTime ,  
           @TaskPriority,
		   @TaskDescription )
	return 1;
	End
	else
	BEGIN
	return -1; -- the id is exists
	End

 End
  else begin return 0; End  --the data is incorrect
 End



GO
/****** Object:  StoredProcedure [dbo].[sp_CheckPassword]    Script Date: 25/06/2016 16:29:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_CheckPassword] 
	-- Add the parameters for the stored procedure here
	@UserName  nvarchar(50),
	@Password  nvarchar(50) ,
	@Name      nvarchar(50) output,
	@Id        int          output
AS
BEGIN

if exists (select * from  [dbo].[Managers] where @UserName=ManagerUserName and @Password=ManagerPassword and ManagerField='head') 
begin
   select  @Name = ManagerName from  [dbo].[Managers] where @UserName=ManagerUserName and @Password=ManagerPassword 
	return 3;
	end

if exists (select  * from  [dbo].[Managers] where @UserName=ManagerUserName and @Password=ManagerPassword) 
begin
   select  @Name = ManagerName from  [dbo].[Managers] where @UserName=ManagerUserName and @Password=ManagerPassword 
   select  @Id   = ManagerId   from  [dbo].[Managers] where @UserName=ManagerUserName and @Password=ManagerPassword 

	return 2;
	end

	if exists (select * from  [dbo].[Employees] where @UserName=EmployeeUserName and @Password=EmployeePassword) 
begin
   select  @Name = EmployeeName from  [dbo].[Employees] where @UserName=EmployeeUserName and @Password=EmployeePassword 
   select  @Id   = EmployeeId   from   [dbo].[Employees] where @UserName=EmployeeUserName and @Password=EmployeePassword 

	return 1;
	end

	return -1;
 end
  






GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllEmployees]    Script Date: 25/06/2016 16:29:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAllEmployees] 
	-- Add the parameters for the stored procedure here
	@ManagerUserName  nvarchar(50) ,
	@ManagerPassword nvarchar(50) 
AS
BEGIN
if exists (select * from Managers where @ManagerUserName=ManagerUserName and @ManagerPassword=ManagerPassword) 
begin
	SELECT  * from [dbo].[Employees] 
	return 1;
	end
	else
	begin
	return -1;
	end

 end
  






GO
/****** Object:  StoredProcedure [dbo].[sp_GetEmployeesByManagerId]    Script Date: 25/06/2016 16:29:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetEmployeesByManagerId] 
	-- Add the parameters for the stored procedure here
	@ManagerUserName  nvarchar(50) ,
	@ManagerPassword nvarchar(50) ,
	@ManagerId int
AS
BEGIN
if exists (select * from Managers where @ManagerUserName=ManagerUserName and @ManagerPassword=ManagerPassword ) 
begin
	SELECT  * from  [dbo].[Employees]
	where @ManagerId=Employees.ManagerId
	return 1;
	end
	else
	begin
--the manager user name or password is incorrect
	return -1;
	end

 end
  







GO
/****** Object:  StoredProcedure [dbo].[sp_GetManagerEmployeesId]    Script Date: 25/06/2016 16:29:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetManagerEmployeesId] 
	-- Add the parameters for the stored procedure here
	@ManagerUserName  nvarchar(50) ,
	@ManagerPassword nvarchar(50) ,
	@ManagerId int
AS
BEGIN
if exists (select * from Managers where @ManagerUserName=ManagerUserName and @ManagerPassword=ManagerPassword) 
begin
	SELECT  EmployeeId from [dbo].[Employees]  where @ManagerId=ManagerId
	return 1;
	end
	else
	begin
	return -1;
	end

 end
  






GO
/****** Object:  StoredProcedure [dbo].[sp_GetManagers]    Script Date: 25/06/2016 16:29:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetManagers] 
	-- Add the parameters for the stored procedure here
	@ManagerUserName  nvarchar(50) ,
	@ManagerPassword nvarchar(50) 
AS
BEGIN
if exists (select * from Managers where @ManagerUserName=ManagerUserName and @ManagerPassword=ManagerPassword ) 
begin
	SELECT  * from  [dbo].[Managers]
	return 1;
	end
	else
	begin
	return -1;
	end

 end
  






GO
/****** Object:  StoredProcedure [dbo].[sp_GetTasksByEmployeeId]    Script Date: 25/06/2016 16:29:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetTasksByEmployeeId] 
	-- Add the parameters for the stored procedure here
	@EmployeeUserName  nvarchar(50) ,
	@EmployeePassword nvarchar(50) ,
	@EmployeeId int
AS
BEGIN
if exists (select * from [dbo].[Employees] where  @EmployeeUserName=[EmployeeUserName] and  @EmployeePassword=[EmployeePassword] ) 
begin
	SELECT  [TaskId] , [TaskPriority], [TaskDescription],[TaskFulfillmentTime]
	       ,[EmployeePermition],[EmployeeDescription],[Status]
	from   [dbo].[Tasks]
	where @EmployeeId=[EmployeeId]
	return 1;
	end
	else
	begin
--the manager user name or password is incorrect
	return -1;
	end

 end
  







GO
/****** Object:  StoredProcedure [dbo].[sp_GetTasksByManagerId]    Script Date: 25/06/2016 16:29:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[sp_GetTasksByManagerId] 
	-- Add the parameters for the stored procedure here
	@ManagerUserName  nvarchar(50) ,
	@ManagerPassword nvarchar(50) ,
	@ManagerId int
AS
BEGIN
if exists (select * from Managers where @ManagerUserName=ManagerUserName and @ManagerPassword=ManagerPassword ) 
begin
	SELECT  * from   [dbo].[Tasks]
	where @ManagerId=[Tasks].ManagerId
	return 1;
	end
	else
	begin
--the manager user name or password is incorrect
	return -1;
	end

 end
  







GO
/****** Object:  StoredProcedure [dbo].[sp_GetTasksIdsAndEmployeesIds]    Script Date: 25/06/2016 16:29:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetTasksIdsAndEmployeesIds] 
	-- Add the parameters for the stored procedure here
	        
		  
		  @ManagerId int
AS
BEGIN
 
 select TaskId,EmployeeId from [dbo].[Tasks]  where @ManagerId=ManagerId
  
 End



GO
/****** Object:  StoredProcedure [dbo].[sp_MoveTask]    Script Date: 25/06/2016 16:29:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[sp_MoveTask] 
	-- Add the parameters for the stored procedure here
	@TaskId int,
	@EmployeeId int
AS
BEGIN
if exists (select * from [dbo].[Tasks] where [TaskId]=@TaskId) 
begin
	update  [dbo].[Tasks]      set  [EmployeeId]=@EmployeeId
	                                      where @TaskId=[TaskId]
	return 1;
	end
	else
	begin
	return -1;
	end

 end
  






GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateEmployeeByManagerId]    Script Date: 25/06/2016 16:29:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_UpdateEmployeeByManagerId] 
	-- Add the parameters for the stored procedure here
	   @nEmployeeId int      --the new employee id
	  ,@oEmployeeId int      --the old employee id
      ,@EmployeeName nvarchar(50)
      ,@EmployeeUserName nvarchar(50)
      ,@EmployeePassword nvarchar(50)
AS
BEGIN
if  exists(select em.EmployeeUserName,ma.ManagerUserName
	           from [dbo].[Employees] em ,[dbo].[Managers] ma
	           where (em.[EmployeeUserName]=@EmployeeUserName or  ma.ManagerUserName=@EmployeeUserName)
			          and  em.EmployeeId<>@oEmployeeId) 
	
	begin
	raiserror('the user name is exists',16,1);
    return;
	end

	if exists (select * from  [dbo].[Employees] where  EmployeeId=@oEmployeeId) 
		begin
		update   [dbo].[Employees]     
		set  [EmployeeId]=@nEmployeeId,[EmployeeName]=@EmployeeName,
		 	 [EmployeeUserName]=@EmployeeUserName,[EmployeePassword]=@EmployeePassword
		where [EmployeeId]=@oEmployeeId
		return 1;
		end
		else
		begin
		return -1;
		end

   
END






GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateTaskByEmployee]    Script Date: 25/06/2016 16:29:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[sp_UpdateTaskByEmployee] 
	-- Add the parameters for the stored procedure here
	   @TaskId int      
      ,@EmployeePermition nvarchar(50) 
      ,@EmployeeDescription nvarchar(50) 
	  ,@Status  nvarchar(50)
AS
BEGIN
if exists (select * from  [dbo].[Tasks]  where   [TaskId]=@TaskId) 
begin
	update   [dbo].[Tasks]    
	set  
	 [EmployeePermition]=@EmployeePermition 
	,[EmployeeDescription]=@EmployeeDescription
	,[Status]=@Status  
	where 
	   [TaskId]=@TaskId
	
	return 1;
	end
	else
	begin
	return -1;
	end

 end
  






GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateTaskByManagerId]    Script Date: 25/06/2016 16:29:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_UpdateTaskByManagerId] 
	-- Add the parameters for the stored procedure here
	   @TaskId int      
      ,@TaskDescription nvarchar(50)
      ,@TaskFulfillmentTime int
      ,@TaskPriority tinyint
	  ,@Status  nvarchar(50)
	  ,@EmployeeId int
AS
BEGIN
if exists (select * from  [dbo].[Employees]   where  [EmployeeId]=@EmployeeId  ) 
if exists (select * from  [dbo].[Tasks]  where   [TaskId]=@TaskId) 
begin
	update   [dbo].[Tasks]    
	set  
	 [TaskId]=@TaskId
	,[TaskDescription]=@TaskDescription 
	,[TaskFulfillmentTime]=@TaskFulfillmentTime
	,[TaskPriority]=@TaskPriority
	,[Status]=@Status  
    ,[EmployeeId]=@EmployeeId
	where 
	   [TaskId]=@TaskId
	
	return 1;
	end
	else
	begin
	return -1;
	end

 end
  





GO
USE [master]
GO
ALTER DATABASE [OperatingSystem] SET  READ_WRITE 
GO
