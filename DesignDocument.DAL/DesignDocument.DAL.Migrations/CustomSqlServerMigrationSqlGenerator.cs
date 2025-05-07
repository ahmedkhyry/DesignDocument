using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;

namespace DesignDocument.DAL.Migrations;

internal class CustomSqlServerMigrationSqlGenerator : SqlServerMigrationSqlGenerator
{
	protected override void Generate(AddColumnOperation addColumnOperation)
	{
		SetCreatedUtcColumn((PropertyModel)(object)addColumnOperation.Column);
		Generate(addColumnOperation);
	}

	protected override void Generate(CreateTableOperation createTableOperation)
	{
		SetCreatedUtcColumn((IEnumerable<ColumnModel>)createTableOperation.Columns);
		Generate(createTableOperation);
	}

	private static void SetCreatedUtcColumn(IEnumerable<ColumnModel> columns)
	{
		foreach (ColumnModel column in columns)
		{
			SetCreatedUtcColumn((PropertyModel)(object)column);
		}
	}

	private static void SetCreatedUtcColumn(PropertyModel column)
	{
		if (column.Name == "CreatedOn")
		{
			column.DefaultValueSql = "GETUTCDATE()";
		}
		if (column.Name == "Id")
		{
			column.DefaultValueSql = "newsequentialid()";
		}
	}
}
