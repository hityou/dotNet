drop table pay;

create table pay( log datetime,  amount int);

-- mssql
--insert into pay values (getdate(), '1234');
--insert into pay values (getdate(), '5678');
-- sqlite
insert into pay values (date('now'),'1234');
insert into pay values (date('now'),'5678');

select * from pay;

select * from sqlite_master where type='table';


