create procedure [dbo].[GetCatById]
	@id UniqueIdentifier
as
	select	c.Id as Id, c.Name as Name,
			a.Id as AbilityId,
			a.Name as AbilityName
	from	Cats as c
			inner join
			Abilities as a
			on c.AbilityId = a.Id
	where	c.Id = @id
return 0
