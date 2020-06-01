drop table paylog;

create table paylog(timestamp date, post text);

insert into paylog values(date('now'),'init post');

select * from paylog;