create procedure dbo.UpdateCat
	@id        uniqueidentifier,
	@name      nvarchar(100),
	@abilityId int
as
begin

update Cats
set    Name = @name,
	   AbilityId = @abilityId
where  Id = @id

return 0
end