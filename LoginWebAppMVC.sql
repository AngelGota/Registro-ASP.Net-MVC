
Create database LoginWebApp
Use LoginWebApp

Create table Users(
idUser int identity (1,1),
namUser varchar(50),
ageUser int,
mailUser varchar(50)
)

Create procedure sp_reg_user
@namUser varchar(50),
@ageUser int,
@mailUser varchar(50) 
as begin
insert into Users values(@namUser, @ageUser,@mailUser)
end

exec sp_reg_user 'Gota',19,'gota@dominio.com'
go

select * from Users

Create procedure sp_rea_user
as begin
select * from Users
end

exec sp_rea_user
