CREATE TABLE [dbo].[ApplicationUserVouch]
(
	[ApplicationUserVouchId] BIGINT NOT NULL CONSTRAINT PK_ApplicationUserVouch PRIMARY KEY,
	[FromApplicationUserId] BIGINT NOT NULL,
	[ToApplicationUserId] BIGINT NOT NULL,
	[Description] NVARCHAR(500) NOT NULL,
	[RowCreationDateTime] DATETIMEOFFSET NOT NULL, 
    [RowCreationUser] NVARCHAR(256) NOT NULL,
    [SourceApplication] NVARCHAR(250) NOT NULL, 
    [OriginatorIPAddress] NVARCHAR(100) NOT NULL
)
