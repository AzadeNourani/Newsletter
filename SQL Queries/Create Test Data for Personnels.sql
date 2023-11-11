USE [NewsletterDb]
GO

DECLARE @Counter INT = 1

WHILE @Counter <= 100
BEGIN
    INSERT INTO [dbo].[Personnels]
           ([FirstName]
           ,[LastName]
           ,[NationalCode])
     VALUES
           ('FirstName' + CAST(@Counter AS NVARCHAR(MAX))
           ,'LastName' + CAST(@Counter AS NVARCHAR(MAX))
           ,'NationalCode' + CAST(@Counter AS NVARCHAR(MAX)))

    SET @Counter = @Counter + 1
END


--====================================================
USE [NewsletterDb]
GO

DECLARE @Counter INT = 1

WHILE @Counter <= 100
BEGIN
    INSERT INTO [dbo].[Personnels]
           ([FirstName]
           ,[LastName]
           ,[NationalCode])
     VALUES
           ('FirstName' + CAST(@Counter AS NVARCHAR(MAX))
           ,'LastName' + CAST(@Counter AS NVARCHAR(MAX))
           ,'NationalCode' + CAST(@Counter AS NVARCHAR(MAX)))

    SET @Counter = @Counter + 1
END
--================================================


