-- جدول اطلاعات پرسنل
CREATE TABLE Personnel (
    PersonnelID INT PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Email NVARCHAR(100),
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
    PersonnelID INT FOREIGN KEY REFERENCES Personnel(PersonnelID),
    NewsletterID INT FOREIGN KEY REFERENCES Newsletter(NewsletterID),
    SendTime DATETIME,
    ReceiveTime DATETIME,
    ViewStatus BIT,
    -- سایر فیلدهای مرتبط با وضعیت ارسال
);
