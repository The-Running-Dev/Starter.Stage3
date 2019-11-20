﻿create procedure [dbo].[GetAllCats]
as

select	c.Id as Id, c.Name, c.SecondaryId,
		a.Id as AbilityId, a.Name as AbilityName
from	Cats as c
		inner join
		Abilities as a
		on c.AbilityId = a.Id