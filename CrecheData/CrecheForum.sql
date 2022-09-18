CREATE TABLE [dbo].[CrecheForum]
(
	[CommentId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[CreatedDate] DateTime not null,
	[Comment] nvarChar(300) not null,
	[UserId] nvarChar(450) not null 
   
)

