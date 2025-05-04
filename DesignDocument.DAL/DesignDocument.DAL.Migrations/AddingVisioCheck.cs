using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Builders;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Resources;

namespace DesignDocument.DAL.Migrations;

[GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
public sealed class AddingVisioCheck : DbMigration, IMigrationMetadata
{
	private readonly ResourceManager Resources = new ResourceManager(typeof(AddingVisioCheck));

	string IMigrationMetadata.Id => "201703041923460_AddingVisioCheck";

	string IMigrationMetadata.Source => null;

	string IMigrationMetadata.Target => Resources.GetString("Target");

	public override void Up()
	{
		((DbMigration)this).AddColumn("dbo.Generations", "IsVisio", (Func<ColumnBuilder, ColumnModel>)((ColumnBuilder c) => c.Boolean((bool?)false, (bool?)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null)), (object)null);
	}

	public override void Down()
	{
		((DbMigration)this).DropColumn("dbo.Generations", "IsVisio", (object)null);
	}
}
