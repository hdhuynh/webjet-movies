namespace Database.Code;

public static class ZeroDb
{
	public static string Script =>
		@"
				--
				DECLARE @SQL nvarchar(max) = '';

				-- check constraints
				SET @SQL = '';
				SELECT @SQL += 'ALTER TABLE ['+SCHEMA_NAME([schema_id])+'].['+OBJECT_NAME([parent_object_id])+'] DROP CONSTRAINT ['+name+'];
				' FROM sys.check_constraints;
				EXEC (@SQL);

				-- foreign keys
				SET @SQL = '';
				SELECT @SQL += 'ALTER TABLE ['+SCHEMA_NAME([schema_id])+'].['+OBJECT_NAME(parent_object_id)+'] DROP CONSTRAINT ['+[name]+'];'
				FROM sys.foreign_keys;
				EXEC (@SQL);

				-- tables
				SET @SQL = '';
				SELECT @SQL += 'DROP TABLE ['+SCHEMA_NAME([schema_id])+'].['+[name]+'];'
				FROM sys.tables;
				EXEC (@SQL);

				-- stored Procs
				SET @SQL = '';
				SELECT @SQL += 'DROP PROCEDURE [' + SCHEMA_NAME(p.schema_id) + '].[' + p.NAME + ']'
				FROM sys.procedures p;
				EXEC (@SQL);
				";
}