create table Pages(id int not null primary key, title varchar(20) not null, path varchar(40) not null, parent int foreign key references Pages(id));


insert into Pages values(0,'Home','index.aspx',null);

create procedure getNavBarItems as select * from Pages where parent is null order by id;



