CREATE PROCEDURE [dbo].[spCrecheForum_GetAll]
	
AS
	SELECT CreatedDate, Comment from [dbo].[CrecheForum]	
RETURN 0
