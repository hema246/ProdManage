create database ProductManagement

create table ProductDetails
(
ProductId int identity primary key,
ProductName varchar(30),
ProdDescription varchar(50),
Quantity int ,
Price int
)


select * from ProductDetails
