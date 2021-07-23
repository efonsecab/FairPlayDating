CREATE TABLE [dbo].[UserPhoto]
(
	[UserPhotoId] BIGINT NOT NULL CONSTRAINT PK_UserPhoto PRIMARY KEY IDENTITY,
	[ApplicationUserId] BIGINT NOT NULL,
	[PhotoBlobUrl] NVARCHAR(500) NOT NULL,

	CONSTRAINT [FK_UserPhoto_ApplicationUser] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser]([ApplicationUserId]), 
)
