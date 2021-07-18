CREATE TABLE [dbo].[UserEyesColorPreference]
(
	[UserEyesColorPreferenceId] BIGINT NOT NULL CONSTRAINT PK_UserEyesColorPreference PRIMARY KEY IDENTITY, 
	[ApplicationUserId] BIGINT NOT NULL,
	[EyesColorId] SMALLINT NOT NULL,

	CONSTRAINT [FK_UserEyesColorPreference_ApplicationUser] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser]([ApplicationUserId]), 
	CONSTRAINT [FK_UserEyesColorPreference_EyesColor] FOREIGN KEY ([EyesColorId]) REFERENCES [EyesColor]([EyesColorId]), 
)
