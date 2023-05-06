-- Deletion of BD
Drop database LoginWebApp
GO
-- Creation of BD
Create database LoginWebApp
Go

Use LoginWebApp
Go

Create table Users(
idUser int identity (1,1),
namUser varchar(50),
ageUser int,
mailUser varchar(50),
passUser varchar(30)
)

Create procedure sp_register_users
@namUser varchar(50),
@ageUser int,
@mailUser varchar(50),
@passUser varchar(30)
as begin
insert into Users values(@namUser, @ageUser,@mailUser,@passUser)
end

exec sp_register_users 'Gota',19,'gota@dominio.com','mypassword'
go

select * from Users

Create procedure sp_read_users
as begin
select * from Users
end

exec sp_read_users
