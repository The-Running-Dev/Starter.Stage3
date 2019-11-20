set identity_insert Abilities on
go

merge into Abilities as target
using (values
	(1, N'Eating'),
	(2, N'Engineering'),
	(3, N'Lounging'),
	(4, N'Napping')
)
as source (Id, Name)
on target.Id = source.Id
when matched then
	update
	set		Name = source.Name
when not matched by target then
	insert (Id, Name)
	values (Id, Name)
when not matched by source then
delete;

set identity_insert Abilities off
go

merge into Cats as target
using (values
	(N'Widget', 1),
	(N'Garfield', 2),
	(N'Mr. Boots', 3)
)
as source (Name, AbilityId)
on target.Name = source.Name
when matched then
	update
	set		Name = source.Name
when not matched by target then
	insert (Name, AbilityId)
	values (Name, AbilityId);