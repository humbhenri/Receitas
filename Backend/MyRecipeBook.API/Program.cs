using MyRecipeBookAPI.Filters;
using MyRecipeBookAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddRouting();
builder.Services.AddControllers();
builder.Services.AddMvc(options => options.Filters.Add(
    typeof(ExceptionFilter)
));

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
