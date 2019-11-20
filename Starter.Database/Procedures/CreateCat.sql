create procedure dbo.CreateCat
	@id		   uniqueidentifier,
	@name      nvarchar(100),
	@abilityId int
as
begin

insert	into Cats
(
	Id,
	Name,
	AbilityId
)
values
(
	@id,
	@name,
	@abilityId
)

end