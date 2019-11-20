create procedure [dbo].[GetAllCats]
as
	select	c.Id, c.Name, c.AbilityId, a.Name as AbilityName
	from	Cats as c
			inner join
			Abilities as a
			on c.AbilityId = a.Id
return 0
