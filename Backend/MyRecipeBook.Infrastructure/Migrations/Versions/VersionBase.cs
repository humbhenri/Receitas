using FluentMigrator;

namespace MyRecipeBook.Infrastructure.Migrations.Versions;

public abstract class VersionBase : Migration
{
    protected FluentMigrator.Builders.Create.Table.ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string tableName)
    {
        return Create.Table(tableName)
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("CreatedOn").AsDateTime().NotNullable()
            .WithColumn("Active").AsBoolean().NotNullable();
    }
}