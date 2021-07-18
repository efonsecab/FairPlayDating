CREATE TABLE [dbo].[UserProfile]
(
	[UserProfileId] BIGINT NOT NULL CONSTRAINT PK_UserProfile PRIMARY KEY IDENTITY,
	[ApplicationUserId] BIGINT NOT NULL CONSTRAINT FK_ApplicationUserId_UserProfile FOREIGN KEY REFERENCES [dbo].[ApplicationUser]([ApplicationUserId]),
	[About] NVARCHAR(100) NOT NULL, 
    [HairColorId] SMALLINT NOT NULL, 
    [EyesColorId] SMALLINT NOT NULL, 
    [KidStatusId] SMALLINT NOT NULL, 
    [PreferredKidStatusId] SMALLINT NOT NULL, 
    CONSTRAINT [FK_UserProfile_HairColor] FOREIGN KEY ([HairColorId]) REFERENCES [HairColor]([HairColorId]), 
    CONSTRAINT [FK_UserProfile_EyesColor] FOREIGN KEY ([EyesColorId]) REFERENCES [EyesColor]([EyesColorId]),
    CONSTRAINT [FK_UserProfile_KidStatus] FOREIGN KEY ([KidStatusId]) REFERENCES [KidStatus]([KidStatusId]),
    CONSTRAINT [FK_UserProfile_PreferredKidStatus] FOREIGN KEY ([PreferredKidStatusId]) REFERENCES [KidStatus]([KidStatusId])
)
