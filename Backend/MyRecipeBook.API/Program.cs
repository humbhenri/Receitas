using MyRecipeBookAPI.Filters;
using MyRecipeBookAPI.Middleware;
using MyRecipeBook.Infrastructure;
using MyRecipeBook.Infrastructure.Migrations;
using MyRecipeBook.Infrastructure.Extensions;
using MyRecipeBookAPI.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddRouting();
builder.Services.AddControllers().AddJsonOptions(options => 
    options.JsonSerializerOptions.Converters.Add(new StringConverter()));
builder.Services.AddMvc(options => options.Filters.Add<ExceptionFilter>());
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddRouting(options => options.LowercaseUrls = true);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseMiddleware<CultureMiddleware>();
app.UseRouting();
app.MapControllers();
MigrateDatabase();
await app.RunAsync();

void MigrateDatabase()
{
    if (builder.Configuration.IsUnitTestEnvironment())
        return;
    var connectionString = app.Configuration.ConnectionString();
    DatabaseMigration.Migrate(connectionString, app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);
}
