CREATE TABLE [dbo].[UserHairColorPreference]
(
	[UserHairColorPreferenceId] BIGINT NOT NULL CONSTRAINT PK_UserHairColorPreference PRIMARY KEY IDENTITY, 
	[ApplicationUserId] BIGINT NOT NULL,
	[HairColorId] SMALLINT NOT NULL,

	CONSTRAINT [FK_UserHairColorPreference_ApplicationUser] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser]([ApplicationUserId]), 
	CONSTRAINT [FK_UserHairColorPreference_HairColor] FOREIGN KEY ([HairColorId]) REFERENCES [HairColor]([HairColorId]), 
)
