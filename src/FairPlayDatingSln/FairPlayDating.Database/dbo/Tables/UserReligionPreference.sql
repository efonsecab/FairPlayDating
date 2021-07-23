CREATE TABLE [dbo].[UserReligionPreference]
(
	[UserReligionPreferenceId] INT NOT NULL CONSTRAINT PK_UserReligionPreference PRIMARY KEY IDENTITY,
	[ApplicationUserId] BIGINT NOT NULL,
	[ReligionId] SMALLINT NOT NULL,
	CONSTRAINT [Fk_UserReligionPreference_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser]([ApplicationUserId]),
	CONSTRAINT [Fk_UserReligionPreference_ReligionId] FOREIGN KEY ([ReligionId]) REFERENCES [Religion]([ReligionId])
)
