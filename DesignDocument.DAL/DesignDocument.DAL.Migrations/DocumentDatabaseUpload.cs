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
public sealed class DocumentDatabaseUpload : DbMigration, IMigrationMetadata
{
	private readonly ResourceManager Resources = new ResourceManager(typeof(DocumentDatabaseUpload));

	string IMigrationMetadata.Id => "201703101216543_DocumentDatabaseUpload";

	string IMigrationMetadata.Source => null;

	string IMigrationMetadata.Target => Resources.GetString("Target");

	public override void Up()
	{
		((DbMigration)this).AddColumn("dbo.Generations", "Document", (Func<ColumnBuilder, ColumnModel>)((ColumnBuilder c) => c.Binary((bool?)null, (int?)null, (bool?)null, (byte[])null, (string)null, false, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null)), (object)null);
	}

	public override void Down()
	{
		((DbMigration)this).DropColumn("dbo.Generations", "Document", (object)null);
	}
}
