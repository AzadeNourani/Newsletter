

ALTER TABLE Personnel
ADD NewPersonnelID UNIQUEIDENTIFIER;


ALTER TABLE Personnel
DROP COLUMN PersonnelID;

EXEC sp_rename 'Personnel.NewPersonnelID', 'PersonnelID', 'COLUMN';
