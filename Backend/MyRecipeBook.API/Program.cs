using MyRecipeBook.Application;
using MyRecipeBookAPI.Filters;
using MyRecipeBookAPI;
using MyRecipeBookAPI.Middleware;
using MyRecipeBook.Infrastructure;
using MyRecipeBook.Infrastructure.Migrations;
using MyRecipeBook.Infrastructure.Extensions;
using MyRecipeBookAPI.Converters;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBookAPI.Token;
using MyRecipeBook.Application.Services.Mappings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi(options =>
{
 options.AddDocumentTransformer<SecuritySchemeTransformer>();
});

builder.Services.AddRouting();
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new StringConverter()));
builder.Services.AddMvc(options => options.Filters.Add<ExceptionFilter>());
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<ITokenProvider, HttpContextValue>();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddHttpContextAccessor();

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
ConfigureMappings();
await app.RunAsync();

void MigrateDatabase()
{
    if (builder.Configuration.IsUnitTestEnvironment())
        return;
    var connectionString = app.Configuration.ConnectionString();
    DatabaseMigration.Migrate(connectionString, app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);
}

void ConfigureMappings()
{
  var alphabet = builder.Configuration.GetValue<string>("Settings:IdCryptographyAlphabet");
  MappingConfigurations.Configure(alphabet!);
}
