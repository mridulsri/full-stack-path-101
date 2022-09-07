using App.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Host.UseAppLogger(configuration);
builder.Services.AddServiceFramework(configuration);
builder.Services.AddDataStore(configuration);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseServiceFramework();
app.UseDataStore();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
