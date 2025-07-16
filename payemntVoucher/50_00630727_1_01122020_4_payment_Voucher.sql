
CREATE TABLE [dbo].[tbl_ServiceRequestDetails](
	[Id] [INT] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[ServicesRequestID] [NVARCHAR](20) NULL,
	[EmpCode] [NVARCHAR](10) NULL,
	[CreatedDate] [DATETIME] NULL,
	[ServicesDescripation] [NVARCHAR](MAX) NULL,
	[FilePath] [NVARCHAR](MAX) NULL,
	[AssignedTo] [NVARCHAR](10) NULL,
	[AssignedDate] [DATETIME] NULL,	
	[Is_Escalated] CHAR(3) NULL,
	[ServiceDepartment] [INT] NULL,
	[Status] [INT] NULL,
	[UpdatedDate] [DATETIME] NULL,
) 

CREATE TABLE [dbo].[tbl_ServiceRequestLog](
	[Id] [INT] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[ServicesRequestID] [NVARCHAR](20) NULL,
	[ServiceDepartment] [INT] NULL,
	[CreatedDate] [DATETIME] NULL,
	[ServicesDescripation] [NVARCHAR](MAX) NULL,
	[FilePath] [NVARCHAR](MAX) NULL,
	[AssignedTo] [NVARCHAR](10) NULL,
	[AssignedDate] [DATETIME] NULL,	
	[ActionStatus] INT NULL,
	[ActionBy] [NVARCHAR](10) NULL,
	[ActionDate] [DATETIME] NULL,
	[Escalation_Type] [CHAR](4) NULL,
	[UpdatedDate] [DATETIME] NULL,
) 


CREATE TABLE [dbo].[tbl_ServiceStatus](
	[Id] [INT] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[StatusName] [NVARCHAR](100) NULL,
	[StatusTitle] [NVARCHAR](100) NULL,
	[CreatedDate] [DATETIME] NULL
) 
--Drop TABLE tbl_SPOCDetail
CREATE TABLE [dbo].[tbl_SPOCDetail](
    [ID] [INT] IDENTITY(1,1) NOT NULL,
	[DEPT_ID] [INT]  NOT NULL,
	[SPOC_ID] [NVARCHAR](10) NOT NULL,
	[AUTO_ESCALATION] [INT] NULL,
	[USER_ESCALATION] [INT] NULL,
	[CreatedDate] [DATETIME] NULL
	constraint PK_SPOCDetail PRIMARY KEY ([DEPT_ID], [SPOC_ID])
) 
USE [HRMS]
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERTSERVICE_REQUEST]    Script Date: 19-01-2021 09:19:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[SP_INSERTSERVICE_REQUEST] --1,1,'2017-11-23','2017-11-23','official','12443',1,2500,'NA','Insert'  
 @qtype VARCHAR(25) = null,
 @id INT =NULL,
 @Services_Request_ID nvarchar(20)=NULL,
 @emp_code NVARCHAR(10) =NULL,  
 @ServicesDescripation NVARCHAR(MAX) = NULL,  
 @FilePath varchar(500)= NULL,  
 @AssignedTo NVARCHAR(10) = NULL,
 @ActionBy NVARCHAR(10) = NULL,
 @Is_Escalated CHAR(3) = NULL,  
 @ServiceDepartment INT = NULL,  
 @Status INT = NULL,
 @ActionStatus INT = NULL,
 @Escalation_Type CHAR(4)=NULL
 AS  
BEGIN  
   declare @maxRemid Numeric(18,0)  
   declare @ServicesRequestID nvarchar(20) = NULL  
   declare @CreatedDate DATETIME = NULL 
   declare @empcode NVARCHAR(10) =NULL
   declare @departmentCode int =NULL
   IF(@qtype='Insert') 
   BEGIN   
     set @maxRemid=(select ISNULL(max(Id+1),1) AS Rem_id from tbl_ServiceRequestDetails)  
     set @ServicesRequestID = CONCAT('SR/', format(CAST(GETDATE() as date),'MM-yyyy'),'/', right('000000'+convert(varchar(20),@maxRemid),6))  
     
	 INSERT INTO tbl_ServiceRequestDetails(ServicesRequestID,EmpCode,CreatedDate,ServicesDescripation,FilePath,AssignedTo,AssignedDate,Is_Escalated,  
     ServiceDepartment,[Status],UpdatedDate)  
	 VALUES (@ServicesRequestID,@emp_code,GETDATE(),@ServicesDescripation,@FilePath,@AssignedTo,GETDATE(),@Is_Escalated
	,@ServiceDepartment,@Status,GETDATE())  
	--Insert Log
	   INSERT INTO tbl_ServiceRequestLog(ServicesRequestID,ServiceDepartment,CreatedDate,ServicesDescripation,FilePath,AssignedTo,AssignedDate,ActionStatus,ActionBy,ActionDate,Escalation_Type,UpdatedDate)  
	   VALUES (@ServicesRequestID,@ServiceDepartment,GETDATE(),@ServicesDescripation,@FilePath,@AssignedTo,GETDATE(),@Status,@ActionBy,GETDATE(),NULL,GETDATE()) 
	--End
       SELECT @maxRemid as maxRemid, 'Service Request submitted successfully' as 'Message'  
      
      End  
      ELSE IF(@qtype='UpdateAction')
	  BEGIN
	  set @CreatedDate=(select CreatedDate from tbl_ServiceRequestDetails WHERE Id=@id)
	   UPDATE tbl_ServiceRequestDetails SET [STATUS]=@Status,AssignedTo=@AssignedTo,AssignedDate=GETDATE(),UpdatedDate=GETDATE() WHERE Id=@id
	   	   
	   INSERT INTO tbl_ServiceRequestLog(ServicesRequestID,ServiceDepartment,CreatedDate,ServicesDescripation,FilePath,AssignedTo,AssignedDate,ActionStatus,ActionDate,Escalation_Type,UpdatedDate)  
	       VALUES (@Services_Request_ID,@ServiceDepartment,@CreatedDate,@ServicesDescripation,@FilePath,@AssignedTo,GETDATE(),@ActionStatus,GETDATE(),NULL,GETDATE()) 
	  END
	  ELSE IF(@qtype='EscalatedAction')
	  BEGIN
	  set @CreatedDate=(select CreatedDate from tbl_ServiceRequestDetails WHERE Id=@id)
	   UPDATE tbl_ServiceRequestDetails SET [STATUS]=@Status,AssignedTo=@AssignedTo,AssignedDate=GETDATE(),UpdatedDate=GETDATE(),Is_Escalated=@Is_Escalated WHERE Id=@id
	   	   
	   INSERT INTO tbl_ServiceRequestLog(ServicesRequestID,ServiceDepartment,CreatedDate,ServicesDescripation,FilePath,AssignedTo,AssignedDate,ActionStatus,ActionDate,Escalation_Type,UpdatedDate)  
	       VALUES (@Services_Request_ID,@ServiceDepartment,@CreatedDate,@ServicesDescripation,@FilePath,@AssignedTo,GETDATE(),@ActionStatus,GETDATE(),@Escalation_Type,GETDATE()) 
	  END	 
	  ELSE IF(@qtype='GETSPOCDETAIL')
	  BEGIN
	    Select * FROM tbl_SPOCDetail WHERE DEPT_ID=@id
	  END
	  ELSE IF(@qtype='GETEMPSERVICELIST')
	  BEGIN
	    SELECT SRD.Id, SRD.ServicesRequestID,CONVERT(NVARCHAR(19),
		SRD.CreatedDate,27) AS ServiceRequestDate,
		EMP.Emp_Name AS EmployeeName,
		(EMP1.first_name+' '+ EMP1.last_name )AS AssignedTo,
		CONVERT(NVARCHAR(19),SRD.AssignedDate,27) AS AssignmentDate,
		(SELECT StatusName FROM tbl_ServiceStatus WHERE Id=SRD.Status) As [Status]
		FROM tbl_ServiceRequestDetails SRD
		JOIN tbl_Employee_Mst EMP ON SRD.EmpCode=EMP.Emp_Code
		LEFT JOIN tbl_Employee_Mst EMP1 ON SRD.AssignedTo=EMP1.Emp_Code
	  END
	  ELSE IF(@qtype='GETDDLFORSERVICECREATE')
	  BEGIN
	    SELECT Dm.Department_id As DepartmentId,DM.Department_Name AS DepartmentName,SPO.SPOC_ID AS SPOCID
		FROM tblDepartmentMaster DM
		JOIN tbl_SPOCDetail SPO ON SPO.DEPT_ID=DM.Department_id
	  END
	  ELSE IF(@qtype='GETApprovelPageDetail')
	  BEGIN
	    SET @empcode=(SELECT EmpCode From tbl_ServiceRequestDetails WHERE Id=@id)
		SET @ServicesRequestID=(SELECT ServicesRequestID From tbl_ServiceRequestLog WHERE Id=@id)
		SET @departmentCode=(SELECT ServiceDepartment From tbl_ServiceRequestDetails WHERE Id=@id)
		--Table 1 Employee Detail
		SELECT EMP.Emp_Name,EMP.Emp_Emailaddress,EMP.Designation,EMP.Department,EMP.Emp_Code,EMP.mobile  FROM tbl_Employee_Mst EMP WHERE Emp_Code=@empcode
		--Table 2 Service Request History
		 SELECT SRL.Id, SS.StatusName,
		 CONVERT(NVARCHAR(19),SRL.CreatedDate,27) AS ReceivedDate,
		 (em.first_name+' '+em.last_name) As ActionBy,
		 CONVERT(NVARCHAR(19),SRL.ActionDate,27) AS ActionDate,
		 SRL.ServiceDepartment,SRL.ServicesDescripation,SRl.ServicesRequestID,SRL.FilePath
		 FROM tbl_ServiceRequestLog SRL
		 JOIN tbl_ServiceStatus SS on SS.Id=SRL.ActionStatus
		 JOIN tbl_Employee_Mst Em ON em.Emp_Code=SRL.ActionBy
		 WHERE ServicesRequestID=@ServicesRequestID
		 --Table 3 DepartMent Detail
		 SELECT Emp_Code,(first_name+' '+last_name) AS EmployeeName FROM tbl_Employee_Mst WHERE emp_status='Onboard' AND dept_id=@departmentCode 
		 --Table 4 Service Request Details
		 SELECT * From tbl_ServiceRequestDetails WHERE Id=@id
	  END
	  ELSE IF(@qtype='GETEMPDEPT')
	  BEGIN
	    Select Department FROM tbl_Employee_Mst WHERE Emp_Code=@emp_code
	  END
	  ELSE
	  BEGIN
	     SELECT 0 as maxRemid, 'No Action Found' as 'Message'
	  END
END  
   