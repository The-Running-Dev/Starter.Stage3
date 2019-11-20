create procedure dbo.DeleteCat
	@id uniqueidentifier
as
begin

delete
from	Cats
where	Id = @id

end