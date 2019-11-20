create procedure dbo.DeleteCat
	@id uniqueidentifier
as

delete
from	Cats
where	Id = @id