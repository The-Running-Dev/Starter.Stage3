create procedure dbo.CreateCat
	@id				uniqueidentifier,
	@secondaryId	uniqueidentifier,
	@name			nvarchar(100),
	@abilityId		int
as
begin

insert	into Cats
(
	Id,
	SecondaryId,
	Name,
	AbilityId
)
values
(
	@id,
	@secondaryId,
	@name,
	@abilityId
)

end