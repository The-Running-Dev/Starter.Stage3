create procedure dbo.UpdateCat
	@id        uniqueidentifier,
	@name      nvarchar(100),
	@abilityId int
as

update Cats
set    Name = @name,
	   AbilityId = @abilityId
where  Id = @id