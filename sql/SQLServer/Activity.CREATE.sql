
/*
-- DELETE

DELETE FROM ActivityRole
DELETE FROM Activity
*/

/*
UPDATE ActivityRole SET Operations = 'I' + Operations WHERE CHARINDEX('S', Operations) > 0
*/

/*
-- DROP

DROP TABLE ActivityRole
DROP TABLE Activity
*/

CREATE TABLE Activity
(
	Id nvarchar(128) NOT NULL
	,Name nvarchar(256) NOT NULL
    ,CONSTRAINT PK_Activity PRIMARY KEY (Id)
)
CREATE INDEX IX_Activity_01 ON Activity(Name)

CREATE TABLE ActivityRole
(
	ActivityId nvarchar(128) NOT NULL
	,RoleName nvarchar(256) NOT NULL
	,Operations varchar(256) NULL
    ,CONSTRAINT PK_ActivityRole PRIMARY KEY (ActivityId, RoleName)
)
ALTER TABLE ActivityRole ADD CONSTRAINT FK_ActivityRole_01
    FOREIGN KEY(ActivityId) REFERENCES Activity(Id) ON UPDATE CASCADE
CREATE INDEX IX_ActivityRole_01 ON ActivityRole(RoleName)
