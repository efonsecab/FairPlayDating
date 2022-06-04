CREATE TABLE [dbo].[ApplicationUserVouch]
(
	[ApplicationUserVouchId] BIGINT NOT NULL CONSTRAINT PK_ApplicationUserVouch PRIMARY KEY,
	[FromApplicationUserId] BIGINT NOT NULL,
	[ToApplicationUserId] BIGINT NOT NULL,
	[Description] NVARCHAR(500) NOT NULL
)
