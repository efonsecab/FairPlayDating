/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
SET IDENTITY_INSERT [dbo].[ApplicationRole] ON
DECLARE @ROLE_USER NVARCHAR(50)  = 'User'
IF NOT EXISTS (SELECT * FROM [dbo].[ApplicationRole] AR WHERE [AR].[Name] = @ROLE_USER)
BEGIN 
    INSERT INTO [dbo].[ApplicationRole]([ApplicationRoleId],[Name],[Description]) VALUES(1, @ROLE_USER, 'Normal Users')
END
SET IDENTITY_INSERT [dbo].[ApplicationRole] OFF

SET IDENTITY_INSERT [dbo].[Frequency] ON
DECLARE @FREQUENCYNAME NVARCHAR(50) = 'Never'
DECLARE @FREQUENCYID SMALLINT = 1
IF NOT EXISTS (SELECT * FROM [dbo].[Frequency] F WHERE [F].[Name] = @FREQUENCYNAME)
BEGIN
    INSERT INTO [dbo].[Frequency]([FrequencyId], [Name]) VALUES(@FREQUENCYID, @FREQUENCYNAME)
END
SET @FREQUENCYNAME = '1 day per week'
SET @FREQUENCYID = 2
IF NOT EXISTS (SELECT * FROM [dbo].[Frequency] F WHERE [F].[Name] = @FREQUENCYNAME)
BEGIN
    INSERT INTO [dbo].[Frequency]([FrequencyId], [Name]) VALUES(@FREQUENCYID, @FREQUENCYNAME)
END
SET @FREQUENCYNAME = '2 days per week'
SET @FREQUENCYID = 3
IF NOT EXISTS (SELECT * FROM [dbo].[Frequency] F WHERE [F].[Name] = @FREQUENCYNAME)
BEGIN
    INSERT INTO [dbo].[Frequency]([FrequencyId], [Name]) VALUES(@FREQUENCYID, @FREQUENCYNAME)
END
SET @FREQUENCYNAME = '3 days per week'
SET @FREQUENCYID = 4
IF NOT EXISTS (SELECT * FROM [dbo].[Frequency] F WHERE [F].[Name] = @FREQUENCYNAME)
BEGIN
    INSERT INTO [dbo].[Frequency]([FrequencyId], [Name]) VALUES(@FREQUENCYID, @FREQUENCYNAME)
END
SET @FREQUENCYNAME = '4 days per week'
SET @FREQUENCYID = 5
IF NOT EXISTS (SELECT * FROM [dbo].[Frequency] F WHERE [F].[Name] = @FREQUENCYNAME)
BEGIN
    INSERT INTO [dbo].[Frequency]([FrequencyId], [Name]) VALUES(@FREQUENCYID, @FREQUENCYNAME)
END
SET @FREQUENCYNAME = '5 days per week'
SET @FREQUENCYID = 6
IF NOT EXISTS (SELECT * FROM [dbo].[Frequency] F WHERE [F].[Name] = @FREQUENCYNAME)
BEGIN
    INSERT INTO [dbo].[Frequency]([FrequencyId], [Name]) VALUES(@FREQUENCYID, @FREQUENCYNAME)
END
SET @FREQUENCYNAME = '6 days per week'
SET @FREQUENCYID = 7
IF NOT EXISTS (SELECT * FROM [dbo].[Frequency] F WHERE [F].[Name] = @FREQUENCYNAME)
BEGIN
    INSERT INTO [dbo].[Frequency]([FrequencyId], [Name]) VALUES(@FREQUENCYID, @FREQUENCYNAME)
END
SET @FREQUENCYNAME = '7 days per week'
SET @FREQUENCYID = 8
IF NOT EXISTS (SELECT * FROM [dbo].[Frequency] F WHERE [F].[Name] = @FREQUENCYNAME)
BEGIN
    INSERT INTO [dbo].[Frequency]([FrequencyId], [Name]) VALUES(@FREQUENCYID, @FREQUENCYNAME)
END
SET IDENTITY_INSERT [dbo].[Frequency] OFF


SET IDENTITY_INSERT [dbo].[Activity] ON
DECLARE @ACTIVITYNAME NVARCHAR(50) = 'Excercise'
DECLARE @ACTIVITYID SMALLINT = 1
IF NOT EXISTS (SELECT * FROM [dbo].[Activity] A WHERE [A].[Name] = @ACTIVITYNAME)
BEGIN
    INSERT INTO [dbo].[Activity]([ActivityId],[Name]) VALUES(@ACTIVITYID, @ACTIVITYNAME)
END
SET @ACTIVITYNAME = 'Smoking'
SET @ACTIVITYID = 2
IF NOT EXISTS (SELECT * FROM [dbo].[Activity] A WHERE [A].[Name] = @ACTIVITYNAME)
BEGIN
    INSERT INTO [dbo].[Activity]([ActivityId],[Name]) VALUES(@ACTIVITYID, @ACTIVITYNAME)
END
SET @ACTIVITYNAME = 'Drinking'
SET @ACTIVITYID = 3
IF NOT EXISTS (SELECT * FROM [dbo].[Activity] A WHERE [A].[Name] = @ACTIVITYNAME)
BEGIN
    INSERT INTO [dbo].[Activity]([ActivityId],[Name]) VALUES(@ACTIVITYID, @ACTIVITYNAME)
END
SET IDENTITY_INSERT [dbo].[Activity] OFF


SET IDENTITY_INSERT [dbo].[KidStatus] ON
DECLARE @KIDSTATUSNAME NVARCHAR(50) = 'Don''t have & Don''t want'
DECLARE @KIDSTATUSID SMALLINT = 1
IF NOT EXISTS (SELECT * FROM [dbo].[KidStatus] k WHERE [K].[Name] = @KIDSTATUSNAME)
BEGIN
    INSERT INTO [dbo].[KidStatus]([KidStatusId],[Name]) VALUES(@KIDSTATUSID, @KIDSTATUSNAME)
END
SET @KIDSTATUSNAME = 'Don''t have & Want'
SET @KIDSTATUSID = 2
IF NOT EXISTS (SELECT * FROM [dbo].[KidStatus] k WHERE [K].[Name] = @KIDSTATUSNAME)
BEGIN
    INSERT INTO [dbo].[KidStatus]([KidStatusId],[Name]) VALUES(@KIDSTATUSID, @KIDSTATUSNAME)
END
SET @KIDSTATUSNAME = 'Have & Don''t want more'
SET @KIDSTATUSID = 3
IF NOT EXISTS (SELECT * FROM [dbo].[KidStatus] k WHERE [K].[Name] = @KIDSTATUSNAME)
BEGIN
    INSERT INTO [dbo].[KidStatus]([KidStatusId],[Name]) VALUES(@KIDSTATUSID, @KIDSTATUSNAME)
END
SET @KIDSTATUSNAME = 'Have & Want more'
SET @KIDSTATUSID = 4
IF NOT EXISTS (SELECT * FROM [dbo].[KidStatus] k WHERE [K].[Name] = @KIDSTATUSNAME)
BEGIN
    INSERT INTO [dbo].[KidStatus]([KidStatusId],[Name]) VALUES(@KIDSTATUSID, @KIDSTATUSNAME)
END
SET IDENTITY_INSERT [dbo].[KidStatus] OFF