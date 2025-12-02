using FluentMigrator;

namespace MyRecipeBook.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_USER, "Create table user")]
public class Version0001 : VersionBase
{
    private const string TableName = "Users";

    public override void Down()
    {
        Delete.Table(TableName);
    }

    public override void Up()
    {
        CreateTable(TableName)
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("Email").AsString().NotNullable()
            .WithColumn("Password").AsString().NotNullable();
    }
}