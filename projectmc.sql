create database chandana2
create table Employees(
Id int identity(1,1) not null,
FirstName varchar(50) not null,
LastName varchar(50) not null,
email varchar(50) not null,
city varchar(50) not null)
select * from Employees