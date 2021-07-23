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

DECLARE @MALE VARCHAR(20) = 'Male'
DECLARE @MALE_ID SMALLINT = 1
DECLARE @FEMALE VARCHAR(20) = 'Female'
DECLARE @FEMALE_ID SMALLINT = 2

IF NOT EXISTS (SELECT 1 FROM [dbo].[Gender] WHERE [Name] = @MALE)
BEGIN
    INSERT INTO [dbo].[Gender]([GenderId],[Name]) VALUES (@MALE_ID,@MALE)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[Gender] WHERE [Name] = @FEMALE)
BEGIN
    INSERT INTO [dbo].[Gender]([GenderId],[Name]) VALUES (@FEMALE_ID,@FEMALE)
END

DECLARE @Catholic VARCHAR(20) = 'Catholic'
DECLARE @Catholic_ID SMALLINT = 1
DECLARE @Christian VARCHAR(20) = 'Christian'
DECLARE @Christian_ID SMALLINT = 2
DECLARE @Jewish VARCHAR(20) = 'Jewish'
DECLARE @Jewish_ID SMALLINT = 3
DECLARE @Muslim VARCHAR(20) = 'Muslim'
DECLARE @Muslim_ID SMALLINT = 4
DECLARE @Other VARCHAR(20) = 'Other'
DECLARE @Other_ID SMALLINT = 4

IF NOT EXISTS (SELECT 1 FROM [dbo].[Religion] WHERE [Name] = @Catholic)
BEGIN
    INSERT INTO [dbo].[Religion]([ReligionId],[Name]) VALUES (@Catholic_ID,@Catholic)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[Religion] WHERE [Name] = @Christian)
BEGIN
    INSERT INTO [dbo].[Religion]([ReligionId],[Name]) VALUES (@Christian_ID,@Christian)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[Religion] WHERE [Name] = @Jewish)
BEGIN
    INSERT INTO [dbo].[Religion]([ReligionId],[Name]) VALUES (@Jewish_ID,@Jewish)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[Religion] WHERE [Name] = @Muslim)
BEGIN
    INSERT INTO [dbo].[Religion]([ReligionId],[Name]) VALUES (@Muslim_ID,@Muslim)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[Religion] WHERE [Name] = @Other)
BEGIN
    INSERT INTO [dbo].[Religion]([ReligionId],[Name]) VALUES (@Other_ID,@Other)
END

DECLARE @FRIENDSHIP VARCHAR(20) = 'Friendship'
DECLARE @FRIENDSHIP_ID SMALLINT = 1
DECLARE @DATING VARCHAR(20) = 'Dating'
DECLARE @DATING_ID SMALLINT = 2
DECLARE @MARRIAGE VARCHAR(20) = 'Marriage'
DECLARE @MARRIAGE_ID SMALLINT = 3

IF NOT EXISTS (SELECT 1 FROM [dbo].[DateObjective] WHERE [Name] = @FRIENDSHIP)
BEGIN
    INSERT INTO [dbo].[DateObjective]([DateObjectiveId],[Name]) VALUES (@FRIENDSHIP_ID,@FRIENDSHIP)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[DateObjective] WHERE [Name] = @DATING)
BEGIN
    INSERT INTO [dbo].[DateObjective]([DateObjectiveId],[Name]) VALUES (@DATING_ID,@DATING)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[DateObjective] WHERE [Name] = @MARRIAGE)
BEGIN
    INSERT INTO [dbo].[DateObjective]([DateObjectiveId],[Name]) VALUES (@MARRIAGE_ID,@MARRIAGE)
END
