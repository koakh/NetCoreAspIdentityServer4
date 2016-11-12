Required to add mannualy migrations table

```
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
```

DROP TABLE movieactor;
DROP TABLE moviereview;
DROP TABLE movie;
DROP TABLE actor;
DROP TABLE posttag;
DROP TABLE post;
DROP TABLE tag;

DELETE FROM actor;
DELETE FROM movie;
DELETE FROM movieactor;
DELETE FROM post;
DELETE FROM posttag;
DELETE FROM moviereview;
DELETE FROM tag;
