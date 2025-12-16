

create proc CreateHostWebsit
	@userid bigint,
	@WebsitName varchar(50)
as
begin
	if exists (select 1 from Websites where Name=@WebsitName and UserId=@userid)
		throw  50000,'The Name is token from user',1
	declare @userName varchar(50); 
	select @userName= REPLACE(FullName,' ','_') from AspNetUsers where Id=@userid

	print @userid

end 


select REPLACE('cdcs cdscs cdsc dscs      cscs        ',' ','_');