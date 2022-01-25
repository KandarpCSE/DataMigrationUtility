delete from MigrationStatuses
delete from Destination


DBCC CHECKIDENT('Destination', RESEED, 0)
DBCC CHECKIDENT('MigrationStatuses', RESEED, 0)