-- جدول اطلاعات پرسنل
CREATE TABLE Personnel (
    PersonnelID INT primary key,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    NationalCode NVARCHAR(10),
    -- سایر فیلدهای مرتبط با پرسنل
);

-- جدول خبرنامه
CREATE TABLE Newsletter (
    NewsletterID INT PRIMARY KEY,
    Title NVARCHAR(100),
    Content NVARCHAR(MAX),
    SendDate DATETIME,
    -- سایر فیلدهای مرتبط با خبرنامه
);

-- جدول ثبت وضعیت ارسال خبرنامه برای هر شخص
CREATE TABLE NewsletterStatus (
    StatusID INT PRIMARY KEY,
    PersonnelID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Personnel(PersonnelID),
    NewsletterID INT FOREIGN KEY REFERENCES Newsletter(NewsletterID),
    SendTime DATETIME,
    ReceiveTime DATETIME,
    ViewStatus BIT,
    -- سایر فیلدهای مرتبط با وضعیت ارسال
);

-- Insert 1000 records into the Personnel table with GUID as PersonnelID
INSERT INTO Personnel (PersonnelID, FirstName, LastName, NationalCode)
SELECT TOP 1000
    NEWID(), -- Use NEWID() to generate a unique identifier (GUID)
    'FirstName' + CAST(ROW_NUMBER() OVER (ORDER BY NEWID()) AS NVARCHAR(10)),
    'LastName' + CAST(ROW_NUMBER() OVER (ORDER BY NEWID()) AS NVARCHAR(10)),
    RIGHT('0000000000' + CAST(ABS(CHECKSUM(NEWID())) % 1000000000 AS NVARCHAR(10)), 10)
FROM master.dbo.spt_values as v1
CROSS JOIN master.dbo.spt_values as v2


CREATE TABLE NewsletterStatus (
    StatusID INT PRIMARY KEY,
    PersonnelID Int FOREIGN KEY REFERENCES Personnel(PersonelID),
    NewsletterID INT FOREIGN KEY REFERENCES Newsletter(NewsletterID),
    SendTime DATETIME,
    ReceiveTime DATETIME,
    ViewStatus BIT,
    -- سایر فیلدهای مرتبط با وضعیت ارسال
);

-- Add foreign key relationship between NewsletterStatus and Personnel
ALTER TABLE NewsletterStatus
ADD CONSTRAINT FK_NewsletterStatus_Personnel
FOREIGN KEY (PersonnelID) REFERENCES Personnel(PersonnelID);
