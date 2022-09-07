using App.Infrastructure.Logging;
using App.Microservices.Categories.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Host.UseAppLogger(configuration);
builder.Services.AddServiceFramework(configuration);
builder.Services.AddDataStore<CategoryDbContext>(configuration);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseServiceFramework();
app.UseDataStoreMigration<CategoryDbContext>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
