using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Sql;

namespace DesignDocument.DAL.Migrations;

internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
{
	public Configuration()
	{
		((DbMigrationsConfiguration)this).AutomaticMigrationsEnabled = false;
		((DbMigrationsConfiguration)this).SetSqlGenerator("System.Data.SqlClient", (MigrationSqlGenerator)(object)new CustomSqlServerMigrationSqlGenerator());
	}

	protected override void Seed(DataContext context)
	{
	}
}
