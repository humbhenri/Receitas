using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace MyRecipeBook.Infrastructure.Migrations;

public class DatabaseMigration
{
    public static void Migrate(string connectionString, IServiceProvider serviceProvider)
    {
        var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString);
        var databaseName = connectionStringBuilder.Database;
        connectionStringBuilder.Remove("Database");
        using var dbConnection = new MySqlConnection(connectionStringBuilder.ConnectionString);
        var parameters = new DynamicParameters();
        parameters.Add("name", databaseName);
        var records = dbConnection.Query("select * from information_schema.schemata where schema_name = @name", parameters);
        if (!records.Any())
        {
            dbConnection.Execute($"create database {databaseName}");
        }
        MigrationDatabase(serviceProvider);
    }

    private static void MigrationDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.ListMigrations();
        runner.MigrateUp();
    }
}