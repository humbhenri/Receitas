using Dapper;
using MySqlConnector;

namespace MyRecipeBook.Infrastructure.Migrations;

public class DatabaseMigration
{
    public static void Migrate(string connectionString)
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
    }
}