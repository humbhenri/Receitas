using MyRecipeBookAPI.Filters;
using MyRecipeBookAPI.Middleware;
using MyRecipeBook.Application;
using MyRecipeBook.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddRouting();
builder.Services.AddControllers();
builder.Services.AddMvc(options => options.Filters.Add(
    typeof(ExceptionFilter)
));
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

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

// app.UseHttpsRedirection();

app.UseMiddleware(typeof(CultureMiddleware));
app.UseRouting();
app.MapControllers();
app.Run();
