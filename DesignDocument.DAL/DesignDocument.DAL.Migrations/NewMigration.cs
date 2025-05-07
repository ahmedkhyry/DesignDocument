using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Builders;
using System.Data.Entity.Migrations.Infrastructure;
using System.Resources;

namespace DesignDocument.DAL.Migrations;

[GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
public sealed class NewMigration : DbMigration, IMigrationMetadata
{
	private readonly ResourceManager Resources = new ResourceManager(typeof(NewMigration));

	string IMigrationMetadata.Id => "201703041718496_NewMigration";

	string IMigrationMetadata.Source => null;

	string IMigrationMetadata.Target => Resources.GetString("Target");

	public override void Up()
	{
		CreateTable("dbo.EntitySelects", (ColumnBuilder c) => new
		{
			Id = c.Guid((bool?)false, true, (Guid?)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			DisplayName = c.String((bool?)null, (int?)null, (bool?)null, (bool?)null, (string)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			LogicalName = c.String((bool?)null, (int?)null, (bool?)null, (bool?)null, (string)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			IsConfigurationEntity = c.Boolean((bool?)false, (bool?)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			Name = c.String((bool?)null, (int?)null, (bool?)null, (bool?)null, (string)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			Notes = c.String((bool?)null, (int?)null, (bool?)null, (bool?)null, (string)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			ModifiedOn = c.DateTime((bool?)false, (byte?)null, (DateTime?)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			CreatedOn = c.DateTime((bool?)false, (byte?)null, (DateTime?)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			Generation_Id = c.Guid((bool?)null, false, (Guid?)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null)
		}, (object)null).PrimaryKey(t => (object)t.Id, (string)null, true, (object)null).ForeignKey("dbo.Generations", t => (object)t.Generation_Id, false, (string)null, (object)null)
			.Index(t => (object)t.Generation_Id, (string)null, false, false, (object)null);
		CreateTable("dbo.Generations", (ColumnBuilder c) => new
		{
			Id = c.Guid((bool?)false, true, (Guid?)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			IP = c.String((bool?)null, (int?)null, (bool?)null, (bool?)null, (string)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			Name = c.String((bool?)null, (int?)null, (bool?)null, (bool?)null, (string)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			Notes = c.String((bool?)null, (int?)null, (bool?)null, (bool?)null, (string)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			ModifiedOn = c.DateTime((bool?)false, (byte?)null, (DateTime?)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			CreatedOn = c.DateTime((bool?)false, (byte?)null, (DateTime?)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			Organization_Id = c.Guid((bool?)null, false, (Guid?)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			User_Id = c.Guid((bool?)null, false, (Guid?)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null)
		}, (object)null).PrimaryKey(t => (object)t.Id, (string)null, true, (object)null).ForeignKey("dbo.Organizations", t => (object)t.Organization_Id, false, (string)null, (object)null)
			.ForeignKey("dbo.Users", t => (object)t.User_Id, false, (string)null, (object)null)
			.Index(t => (object)t.Organization_Id, (string)null, false, false, (object)null)
			.Index(t => (object)t.User_Id, (string)null, false, false, (object)null);
		CreateTable("dbo.Organizations", (ColumnBuilder c) => new
		{
			Id = c.Guid((bool?)false, true, (Guid?)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			URL = c.String((bool?)null, (int?)null, (bool?)null, (bool?)null, (string)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			Name = c.String((bool?)null, (int?)null, (bool?)null, (bool?)null, (string)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			Notes = c.String((bool?)null, (int?)null, (bool?)null, (bool?)null, (string)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			ModifiedOn = c.DateTime((bool?)false, (byte?)null, (DateTime?)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			CreatedOn = c.DateTime((bool?)false, (byte?)null, (DateTime?)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null)
		}, (object)null).PrimaryKey(t => (object)t.Id, (string)null, true, (object)null);
		CreateTable("dbo.Users", (ColumnBuilder c) => new
		{
			Id = c.Guid((bool?)false, true, (Guid?)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			Password = c.String((bool?)null, (int?)null, (bool?)null, (bool?)null, (string)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			Email = c.String((bool?)null, (int?)null, (bool?)null, (bool?)null, (string)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			Phone = c.String((bool?)null, (int?)null, (bool?)null, (bool?)null, (string)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			Name = c.String((bool?)null, (int?)null, (bool?)null, (bool?)null, (string)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			Notes = c.String((bool?)null, (int?)null, (bool?)null, (bool?)null, (string)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			ModifiedOn = c.DateTime((bool?)false, (byte?)null, (DateTime?)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			CreatedOn = c.DateTime((bool?)false, (byte?)null, (DateTime?)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null),
			Organization_Id = c.Guid((bool?)null, false, (Guid?)null, (string)null, (string)null, (string)null, (IDictionary<string, AnnotationValues>)null)
		}, (object)null).PrimaryKey(t => (object)t.Id, (string)null, true, (object)null).ForeignKey("dbo.Organizations", t => (object)t.Organization_Id, false, (string)null, (object)null)
			.Index(t => (object)t.Organization_Id, (string)null, false, false, (object)null);
	}

	public override void Down()
	{
		DropForeignKey("dbo.Generations", "User_Id", "dbo.Users", (object)null);
		DropForeignKey("dbo.Generations", "Organization_Id", "dbo.Organizations", (object)null);
		DropForeignKey("dbo.Users", "Organization_Id", "dbo.Organizations", (object)null);
		DropForeignKey("dbo.EntitySelects", "Generation_Id", "dbo.Generations", (object)null);
		DropIndex("dbo.Users", new string[1] { "Organization_Id" }, (object)null);
		DropIndex("dbo.Generations", new string[1] { "User_Id" }, (object)null);
		DropIndex("dbo.Generations", new string[1] { "Organization_Id" }, (object)null);
		DropIndex("dbo.EntitySelects", new string[1] { "Generation_Id" }, (object)null);
		DropTable("dbo.Users", (object)null);
		DropTable("dbo.Organizations", (object)null);
		DropTable("dbo.Generations", (object)null);
		DropTable("dbo.EntitySelects", (object)null);
	}
}
